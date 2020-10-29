using System.Threading.Tasks;

namespace CTeleport.Services.Interfaces
{
    public interface IHttpService
    {
        Task<T> GetAsync<T>(string url);
    }
}
