using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.City;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.City.RequestDelete
{
    public class ResponseBadRequest
    {
        private CitiesController _controler;

        [Fact(DisplayName = "E possivel realizar o delete")]
        public async Task DeleteResponse()
        {
            var serviceMock = new Mock<ICityService>();

            serviceMock.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(false);

            _controler = new CitiesController(serviceMock.Object);
            _controler.ModelState.AddModelError("Id", "Formato inválido");

            var result = await _controler.Delete(Guid.Empty);
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
