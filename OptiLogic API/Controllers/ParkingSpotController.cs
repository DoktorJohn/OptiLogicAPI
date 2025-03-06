using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptiLogic_API.Model;
using OptiLogic_API.Service;
using System.Text.Json;

namespace OptiLogic_API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ParkingSpotController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly AppDbContext _context;
        private CarService _carService;

        public ParkingSpotController(ILogger<CarController> logger, AppDbContext context, CarService carService)
        {
            _logger = logger;
            _context = context;
            _carService = carService;
        }


        //API-kald returnerer en liste af parkeringsspots for en parkeringsplads
        [HttpGet("parking_slot/{parkingLotId}")]
        public async Task<IActionResult> GetSpots(int parkingLotId)
        {
            List<ParkingSpot> spots = await _context.ParkingSpots
                .Where(x => x.ParkingLotId == parkingLotId)
                .ToListAsync();

            return Ok(JsonSerializer.Serialize(spots));
        }

        //API-kald returnerer en liste af parkeringsspots på parkeringsplads med ID, som er "occupied = true" 
        [HttpGet("parking_slots_occupied/{parkingLotId}")]
        public async Task<IActionResult> GetOccupiedSpots(int parkingLotId)
        {
            List<ParkingSpot> spots = await _context.ParkingSpots
                .Where(x => x.Occupied == true && x.ParkingLotId == parkingLotId)
                .ToListAsync();

            return Ok(JsonSerializer.Serialize(spots));
        }
    }
}
