using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.City;
using Api.Domain.Interfaces.Services.City;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.City.RequestPut
{
    public class ResponseDeleted
    {
        private CitiesController _controler;

        [Fact(DisplayName = "E possivel realizar o delete")]
        public async Task DeleteResponse()
        {
            var serviceMock = new Mock<ICityService>();

            serviceMock
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            _controler = new CitiesController(serviceMock.Object);

            var result = await _controler.Delete(Guid.NewGuid());
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult) result).Value;
            Assert.NotNull (resultValue);
            Assert.True((Boolean) resultValue);
        }
    }
}
