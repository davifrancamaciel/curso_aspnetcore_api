using System;
using System.Threading.Tasks;
using Api.Domain.DTOs.City;
using Api.Domain.Interfaces.Services.City;
using Moq;
using Xunit;

namespace Api.Service.Test.City
{
    public class CityGet : CityTests
    {
        private ICityService _service;

        private Mock<ICityService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método get")]
        public async Task GetCity()
        {
            _serviceMock = new Mock<ICityService>();
            _serviceMock.Setup(x => x.Get(Id)).ReturnsAsync(cityDto);
            _service = _serviceMock.Object;

            var result = await _service.Get(Id);

            Assert.NotNull(result);
            Assert.True(result.Id == Id);
            Assert.Equal(Name, result.Name);

            _serviceMock = new Mock<ICityService>();
            _serviceMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(Task.FromResult((CityDTO)null));
            _service = _serviceMock.Object;

            var _record = await _service.Get(Id);
            Assert.Null(_record);
        }
    }
}
