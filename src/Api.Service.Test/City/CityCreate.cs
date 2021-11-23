
using System;
using System.Threading.Tasks;
using Api.Domain.DTOs.City;
using Api.Domain.Interfaces.Services.City;
using Moq;
using Xunit;

namespace Api.Service.Test.City
{
    public class CityCreate : CityTests
    {
        private ICityService _service;

        private Mock<ICityService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método de criar cidade")]
        public async Task CreateCities()
        {
            _serviceMock = new Mock<ICityService>();
            _serviceMock.Setup(x => x.Post(cityCreateDto)).ReturnsAsync(cityCreateResultDto);
            _service = _serviceMock.Object;

            var result = await _service.Post(cityCreateDto);

            Assert.NotNull(result);
            Assert.True(result.Id != Guid.Empty);
            Assert.Equal(Name, result.Name);
            Assert.Equal(IbgeId, result.IbgeId);
            Assert.Equal(StateId, result.StateId);
        }
    }
}
