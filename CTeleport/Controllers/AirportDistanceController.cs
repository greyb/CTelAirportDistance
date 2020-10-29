using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CTeleport.Models;
using CTeleport.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTeleport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirportDistanceController : ControllerBase
    {
        private readonly IAirportDistanceService _airportDistanceService;

        public AirportDistanceController(
            IAirportDistanceService airportDistanceService)
        {
            _airportDistanceService = airportDistanceService;
        }

        [HttpGet("{iataCode1}/{iataCode2}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string iataCode1, string iataCode2)
        {
            if (!IsIataValid(iataCode1) || !IsIataValid(iataCode2))
                return this.BadRequest("Iata code is invalid");

            var distance = await _airportDistanceService.GetAirportDistanceAsync(iataCode1, iataCode2).ConfigureAwait(false);
            
            return Ok(new AirportDistanceResponse { Distance = distance });
        }

        private static bool IsIataValid(string iataCode)
        {
            const string iataRegex = @"^[A-Z]{3}$";
            return new Regex(iataRegex).IsMatch(iataCode);
        }
    }
}
