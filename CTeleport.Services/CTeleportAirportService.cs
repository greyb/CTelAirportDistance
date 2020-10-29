using CTeleport.Services.Interfaces;
using System.Threading.Tasks;

namespace CTeleport.Services
{
    public class CTeleportAirportService : IAirportService
    {
        private readonly IHttpService _httpService;
        private readonly string _cteleportServiceBaseUrl;

        public CTeleportAirportService(
            IHttpService httpService,
            string cteleportServiceBaseUrl)
        {
            _httpService = httpService;
            _cteleportServiceBaseUrl = cteleportServiceBaseUrl;
        }

        public async Task<AirportInfo> GetAirportInfoAsync(string iataCode)
        {
            var response = await _httpService.GetAsync<CTeleportAirportInfo>($"{_cteleportServiceBaseUrl}/airports/{iataCode}");

            return ConvertAirportInfo(response);
        }

        private static AirportInfo ConvertAirportInfo(CTeleportAirportInfo airportInfo)
        {
            return new AirportInfo
            {
                Iata = airportInfo.Iata,
                Latitude = airportInfo.Location.Lat,
                Longitude = airportInfo.Location.Lon
            };
        }
    }

    internal class CTeleportAirportInfo
    {
        public string Iata { get; set; }
        public CTeleportAirportLocation Location { get; set; }
    }

    internal class CTeleportAirportLocation
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
