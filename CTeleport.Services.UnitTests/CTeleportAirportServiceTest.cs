using CTeleport.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CTeleport.Services.UnitTests
{
    public class CTeleportAirportServiceTest
    {
        private Mock<IHttpService> _httpMock;

        private IAirportService cteleportAirportService;

        [SetUp]
        public void Setup()
        {
            _httpMock = new Mock<IHttpService>();
        }

        [Test]
        public async Task GetAirportInfoAsync_PassCode_ConvertResponseAndReturnCorrectly()
        {
            var expectedUrl = "testUrl";
            var expectedCode = "testCode";

            var response = new CTeleportAirportInfo
            {
                Iata = "IataTst",
                Location = new CTeleportAirportLocation
                {
                    Lon = 100500,
                    Lat = 500100
                }
            };

            _httpMock
                .Setup(s => s.GetAsync<CTeleportAirportInfo>(It.Is<string>(it => it == $"{expectedUrl}/airports/{expectedCode}")))
                .Returns(Task.FromResult(response))
                .Verifiable();

            cteleportAirportService = new CTeleportAirportService(_httpMock.Object, expectedUrl);
            var result = await cteleportAirportService.GetAirportInfoAsync(expectedCode);

            Assert.IsTrue(AirportInfoEqualsResponse(response, result));
            _httpMock.VerifyAll();
        }

        private static bool AirportInfoEqualsResponse(CTeleportAirportInfo response, AirportInfo airportInfo)
        {
            return
                airportInfo.Iata == response.Iata &&
                airportInfo.Longitude == response.Location.Lon &&
                airportInfo.Latitude == response.Location.Lat;

        }
    }
}