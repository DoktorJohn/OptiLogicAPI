using Azure.Core;
using Microsoft.EntityFrameworkCore;
using OptiLogic_API.Controllers;
using OptiLogic_API.HelperModel;
using OptiLogic_API.Model;

namespace OptiLogic_API.Service
{
    public class PredictionService
    {
        private readonly ILogger<CarController> _logger;
        private readonly AppDbContext _context;

        public PredictionService(ILogger<CarController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //Returnerer en liste af biler som eksisterer i databasen.
        public async Task<List<Car>> GetPreviousCars()
        {
            var cars = await _context.Cars
                    .AsNoTracking()
                    .ToListAsync();

            return cars;
        }

        //Opretter bil objekter på baggrund af ML prediction "request", og returnerer listen af dem.
        public List<Car> InstantiateCarObjectsFromRequest(PredictionRequest request)
        {
            var newPrediction = request.Predictions
                .Select(p => new Car
                {
                    CarId = p.Car_Id,
                    XAxis = p.X - (p.Width / 2),
                    YAxis = p.Y - (p.Height / 2),
                    Width = p.Width,
                    Height = p.Height,
                    Confidence = p.Confidence,
                    ParkingLotId = 1
                })
                .ToList();


            return newPrediction;
        }
    }
}
