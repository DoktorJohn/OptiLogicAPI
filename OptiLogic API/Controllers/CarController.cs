using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OptiLogic_API.Model;
using OptiLogic_API.HelperModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OptiLogic_API.Utility;
using OptiLogic_API.Service;
using System.Text.Json;


namespace OptiLogic_API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly AppDbContext _context;
        private CarService _carService;
        private PredictionService _predictionService;
        private CarFilter _carFilter;
        private ParkingSpotService _parkingSpotService;

        public CarController(ILogger<CarController> logger, AppDbContext context, CarService carService, PredictionService predictionService, CarFilter carFilter, ParkingSpotService parkingSpotService)
        {
            _logger = logger;
            _context = context;
            _carService = carService;
            _predictionService = predictionService;
            _carFilter = carFilter;
            _parkingSpotService = parkingSpotService;
        }


        //API-kald returnerer en liste af alle biler i databasen
        [HttpGet("Cars")]
        public async Task<IActionResult> GetCars()
        {
            List<Car> CarDatabaseList = await _context.Cars.ToListAsync(); //Laver liste af biler fra databasen

            return Ok(JsonSerializer.Serialize(CarDatabaseList));

        }

        //API-kald for ML predictions, bruges kun af script.
        [HttpPost]
        public async Task<IActionResult> UpdateDatabase([FromBody] PredictionRequest request)
        {
            try
            {
                const double overlapRatio = 0.40; //Justerer overlap ratio på bilen på en parkeringsplads

                //Skal gemme 2 lister, som vi senere skal bruge.
                var oldPrediction = await _predictionService.GetPreviousCars(); //En liste af bilerne fra det tidligere request, og gemmer dem i en liste.
                var newPrediction = _predictionService.InstantiateCarObjectsFromRequest(request); //Instantierer den nye request's biler, og gemmer dem i en liste.


                var currentCars = _carFilter.CarsToAdd(newPrediction, oldPrediction); //Metode som skal returnere en liste af biler, som skal tilføjes til databasen.
                var carsToRemove = _carFilter.CarsToRemove(oldPrediction, newPrediction); //Metoden som skal returnere en liste af biler, som skal slettes fra databasen.

                await _carService.UpdateCarDatabase(currentCars, carsToRemove); //Opdaterer databasen


                var parkingSpots = await _context.ParkingSpots.ToListAsync(); //Metode som returnerer en liste af parkeringsspot

                foreach (var car in currentCars) //Går hver bil igennem
                {
                    var parkingSpotId = _carService.GetParkingSpotIdForCar(car, parkingSpots, overlapRatio); //Gemmer parkeringsspot ID hvor bilen holder
                    await _parkingSpotService.UpdateParkingSpot(car, parkingSpotId); //Gemmer i databasen at bilen holder på en parkeringsplads og hvorhenne den holder.
                }

                return Ok(request);

            }


            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.Message);
            }
;
        }

    }

}
