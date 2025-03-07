using Microsoft.EntityFrameworkCore;
using OptiLogic_API.Controllers;
using OptiLogic_API.Model;

namespace OptiLogic_API.Service
{
    public class CarService
    {
        private readonly ILogger<CarController> _logger;
        private readonly AppDbContext _context;

        public CarService(ILogger<CarController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public async Task UpdateCarDatabase(List<Car> carsToAdd, List<Car> carsToRemove)
        {
            if (carsToRemove.Any())
            {
                var parkingSpotsOccupied = await _context.ParkingSpots.Where(p => p.Occupied == true).ToListAsync();

                foreach (var parkingSpot in parkingSpotsOccupied)
                {
                    if (carsToRemove.Any(x => x.CarId == parkingSpot.CarId))
                    {
                        parkingSpot.Occupied = false;
                        parkingSpot.CarId = null;
                    }
                }

                _context.Cars.RemoveRange(carsToRemove);
            }

            if (carsToAdd.Any())
            {
                foreach (var car in carsToAdd)
                {
                    var existingCar = await _context.Cars
                        .AsNoTracking()
                        .FirstOrDefaultAsync(c => c.CarId == car.CarId);

                    if (existingCar == null)
                    {
                        _context.Cars.Add(car);
                    }
                    else
                    {
                        _context.Entry(existingCar).State = EntityState.Detached;
                        _context.Cars.Update(car);
                    }
                }
            }

            await _context.SaveChangesAsync();
        }


        public int GetParkingSpotIdForCar(Car car, List<ParkingSpot> spots, double minOverlapRatio)
        {
            foreach (var spot in spots)
            {
                double xLeft = Math.Max(car.XAxis, spot.XAxis);
                double yTop = Math.Max(car.YAxis, spot.YAxis);
                double xRight = Math.Min(car.XAxis + car.Width, spot.XAxis + spot.Width);
                double yBottom = Math.Min(car.YAxis + car.Height, spot.YAxis + spot.Height);

                if (xRight <= xLeft || yBottom <= yTop)
                    continue;

                double intersectionArea = (xRight - xLeft) * (yBottom - yTop);
                double carArea = car.Width * car.Height;

                if ((intersectionArea / carArea) >= minOverlapRatio)
                {
                    return spot.ParkingSpotID;
                }
            }

            return 0;
        }


    }
}
