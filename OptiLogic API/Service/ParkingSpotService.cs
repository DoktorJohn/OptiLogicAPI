using OptiLogic_API.Controllers;
using OptiLogic_API.Model;

namespace OptiLogic_API.Service
{
    public class ParkingSpotService
    {
        private readonly ILogger<CarController> _logger;
        private readonly AppDbContext _context;


        public ParkingSpotService(ILogger<CarController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //Opdaterer en parkeringsplads' parkeringsspots til occupied true/false, og hvilken bil som pt. holder på dem.
        public async Task UpdateParkingSpot(Car car, int parkingSpotId)
        {
            if (parkingSpotId > 0)
            {
                var spot = await _context.ParkingSpots.FindAsync(parkingSpotId);
                if (spot != null)
                {
                    spot.Occupied = true;
                    spot.CarId = car.CarId;
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
