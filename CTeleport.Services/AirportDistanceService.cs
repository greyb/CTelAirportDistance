using CTeleport.Services.Interfaces;

using System.Threading.Tasks;

namespace CTeleport.Services
{
    public class AirportDistanceService : IAirportDistanceService
    {
        private readonly IDistanceService _distanceService;
        private readonly IAirportService _airportService;

        public AirportDistanceService(
            IDistanceService distanceService,
            IAirportService airportService)
        {
            _distanceService = distanceService;
            _airportService = airportService;
        }

        public async Task<double> GetAirportDistanceAsync(string iataCode1, string iataCode2)
        {
            var airport1 =_airportService.GetAirportInfoAsync(iataCode1);
            var airport2 = _airportService.GetAirportInfoAsync(iataCode2);

            var airportsData = await Task.WhenAll(airport1, airport2).ConfigureAwait(false);

            return _distanceService.GetDistance(
                new LatLon(airportsData[0].Latitude, airportsData[0].Longitude),
                new LatLon(airportsData[1].Latitude, airportsData[1].Longitude));

        }
    }
}
