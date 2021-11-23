
using System;
using System.Threading.Tasks;
using Api.Domain.DTOs.City;
using Api.Domain.Interfaces.Services.City;
using Moq;
using Xunit;

namespace Api.Service.Test.City
{
    public class CityUpdate : CityTests
    {
        private ICityService _service;

        private Mock<ICityService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método de alterar cidade")]
        public async Task UpdateCities()
        {
            _serviceMock = new Mock<ICityService>();
            _serviceMock.Setup(x => x.Put(cityUpdateDto)).ReturnsAsync(cityUpdateResultDto);
            _service = _serviceMock.Object;

            var resultUpdate = await _service.Put(cityUpdateDto);

            Assert.NotNull(resultUpdate);
            Assert.True(resultUpdate.Id != Guid.Empty);
            Assert.Equal(Name, resultUpdate.Name);
            Assert.Equal(IbgeId, resultUpdate.IbgeId);
            Assert.Equal(StateId, resultUpdate.StateId);
        }
    }
}
