using CTeleport.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CTeleport.Services.UnitTests
{
    public class AirportDistanceServiceTest
    {
        private Mock<IAirportService> _airportServiceMock;
        private Mock<IDistanceService> _distanceServiceMock;

        private IAirportDistanceService airportDistanceService;

        [SetUp]
        public void Setup()
        {
            _airportServiceMock = new Mock<IAirportService>();
            _distanceServiceMock = new Mock<IDistanceService>();

            airportDistanceService = new AirportDistanceService(_distanceServiceMock.Object, _airportServiceMock.Object);
        }

        [Test]
        public async Task GetAirportDistanceAsync_PassCodes_ReturnExpectedDistance()
        {
            var testCode1 = "testCode1";
            var testCode2 = "testCode2";

            var testAirportInfo1 = new AirportInfo { Longitude = 1, Latitude = 2 };
            var testAirportInfo2 = new AirportInfo { Longitude = 3, Latitude = 4 };

            var expectedDistance = 100500.1;

            _airportServiceMock
                .Setup(s => s.GetAirportInfoAsync(It.Is<string>(it => it == testCode1)))
                .Returns(Task.FromResult(testAirportInfo1))
                .Verifiable();

            _airportServiceMock
                .Setup(s => s.GetAirportInfoAsync(It.Is<string>(it => it == testCode2)))
                .Returns(Task.FromResult(testAirportInfo2))
                .Verifiable();

            _distanceServiceMock
                .Setup(s => s.GetDistance(
                    It.Is<LatLon>(it => it.Latitude == testAirportInfo1.Latitude && it.Longitude == testAirportInfo1.Longitude),
                    It.Is<LatLon>(it => it.Latitude == testAirportInfo2.Latitude && it.Longitude == testAirportInfo2.Longitude)
                ))
                .Returns(expectedDistance)
                .Verifiable();

            var result = await airportDistanceService.GetAirportDistanceAsync(testCode1, testCode2);

            Assert.AreEqual(expectedDistance, result);
            _airportServiceMock.VerifyAll();
            _distanceServiceMock.VerifyAll();
        }
    }
}