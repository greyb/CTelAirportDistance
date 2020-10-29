using CTeleport.Controllers;
using CTeleport.Models;
using CTeleport.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CTeleport.UnitTests
{
    public class AirportDistanceControllerTest
    {
        private Mock<IAirportDistanceService> _airportDistanceServiceMock;

        private AirportDistanceController airportDistanceController;

        [SetUp]
        public void Setup()
        {
            _airportDistanceServiceMock = new Mock<IAirportDistanceService>();

            airportDistanceController = new AirportDistanceController(_airportDistanceServiceMock.Object);
        }

        [Test]
        public async Task Get_PassCodes_ReturnExpectedDistance()
        {
            var testCode1 = "TST";
            var testCode2 = "STS";

            var expectedDistance = 100500.1;

            _airportDistanceServiceMock
                .Setup(s => s.GetAirportDistanceAsync(It.Is<string>(it => it == testCode1), It.Is<string>(it => it == testCode2)))
                .Returns(Task.FromResult(expectedDistance))
                .Verifiable();

            var result = (await airportDistanceController.Get(testCode1, testCode2)) as OkObjectResult;

            Assert.IsNotNull(result);
            var response = (result.Value as AirportDistanceResponse);
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedDistance, response.Distance);
            _airportDistanceServiceMock.VerifyAll();
        }

        [Test]
        public async Task Get_PassBadFirstParam_ReturnBadRequest()
        {
            var testCode1 = "T";
            var testCode2 = "STS";

            var result = (await airportDistanceController.Get(testCode1, testCode2)) as BadRequestObjectResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Get_PassBadSecondParam_ReturnBadRequest()
        {
            var testCode1 = "TST";
            var testCode2 = "S";

            var result = (await airportDistanceController.Get(testCode1, testCode2)) as BadRequestObjectResult;

            Assert.IsNotNull(result);
        }
    }
}