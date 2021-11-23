using System;
using System.Threading.Tasks;
using Api.Domain.DTOs.City;
using Api.Domain.Interfaces.Services.City;
using Moq;
using Xunit;

namespace Api.Service.Test.City
{
    public class CityGetCompleteById : CityTests
    {
        private ICityService _service;

        private Mock<ICityService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método GetCompleteById")]
        public async Task GetCompleteById()
        {
            _serviceMock = new Mock<ICityService>();
            _serviceMock.Setup(x => x.GetCompleteById(Id)).ReturnsAsync(cityCompleteDto);
            _service = _serviceMock.Object;

            var result = await _service.GetCompleteById(Id);

            Assert.NotNull(result);
            Assert.True(result.Id == Id);
            Assert.Equal(Name, result.Name);
            Assert.Equal(Id, result.Id);
            Assert.Equal(StateId, result.StateId);
            Assert.NotNull(result.State);
        }
    }
}
