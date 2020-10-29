using CTeleport.Services;
using CTeleport.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CTeleport
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IHttpService, HttpService>();
            services.AddSingleton<IDistanceService, HaversineDistanceService>();

            // Decorate original service injection by cache
            services.AddSingleton(
                s => new CTeleportAirportService(s.GetRequiredService<IHttpService>(), Configuration["CTeleportServiceBaseUrl"]));
            services.AddSingleton<IAirportService>(
                s => new AirportServiceCache(s.GetRequiredService<CTeleportAirportService>()));

            services.AddSingleton<IAirportDistanceService, AirportDistanceService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
