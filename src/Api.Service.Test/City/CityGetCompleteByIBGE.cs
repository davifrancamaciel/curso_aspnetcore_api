using System;
using System.Threading.Tasks;
using Api.Domain.DTOs.City;
using Api.Domain.Interfaces.Services.City;
using Moq;
using Xunit;

namespace Api.Service.Test.City
{
    public class CityGetCompleteByIBGE : CityTests
    {
        private ICityService _service;

        private Mock<ICityService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método GetCompleteByIBGE")]
        public async Task GetCompleteByIBGE()
        {
            _serviceMock = new Mock<ICityService>();
            _serviceMock.Setup(x => x.GetCompleteByIBGE(IbgeId)).ReturnsAsync(cityCompleteDto);
            _service = _serviceMock.Object;

            var result = await _service.GetCompleteByIBGE(IbgeId);

            Assert.NotNull(result);
            Assert.True(result.Id == Id);
            Assert.Equal(Name, result.Name);
            Assert.Equal(IbgeId, result.IbgeId);
            Assert.Equal(StateId, result.StateId);
            Assert.NotNull(result.State);
        }
    }
}
