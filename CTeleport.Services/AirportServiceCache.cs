using CTeleport.Services.Interfaces;
using System;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace CTeleport.Services
{
    public class AirportServiceCache : IAirportService
    {
        private readonly IAirportService _airportService;

        public AirportServiceCache(
            IAirportService airportService)
        {
            _airportService = airportService;
        }

        public async Task<AirportInfo> GetAirportInfoAsync(string iataCode)
        {
            ObjectCache cache = MemoryCache.Default;

            var airportInfo = cache[iataCode] as AirportInfo;

            if (airportInfo is null)
            {
                airportInfo = await _airportService.GetAirportInfoAsync(iataCode).ConfigureAwait(false);

                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration =
                    DateTimeOffset.Now.AddHours(1);

                cache.Set(iataCode, airportInfo, policy);
            }

            return airportInfo;
        }
    }
}
