using System;
using System.Threading.Tasks;
using Api.Domain.DTOs.City;
using Api.Domain.Interfaces.Services.City;
using Moq;
using Xunit;

namespace Api.Service.Test.City
{
    public class CityDelete : CityTests
    {
        private ICityService _service;

        private Mock<ICityService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método delete")]
        public async Task DeleteCity()
        {
            _serviceMock = new Mock<ICityService>();
            _serviceMock
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(true);
            _service = _serviceMock.Object;

            var result = await _service.Delete(Id);

            Assert.True(result);            
        }
    }
}
