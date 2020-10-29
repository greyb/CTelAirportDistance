using System.Threading.Tasks;

namespace CTeleport.Services.Interfaces
{
    public interface IAirportService
    {
        Task<AirportInfo> GetAirportInfoAsync(string iataCode);
    }
}
