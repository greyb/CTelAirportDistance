using System.Threading.Tasks;

namespace CTeleport.Services.Interfaces
{
    public interface IAirportDistanceService
    {
        Task<double> GetAirportDistanceAsync(string iataCode1, string iataCode2);
    }
}
