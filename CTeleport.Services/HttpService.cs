using CTeleport.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CTeleport.Services
{
    public class HttpService : IHttpService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<T> GetAsync<T>(string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(responseBody);
        }
    }
}
