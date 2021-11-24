using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.State;
using Api.Domain.Interfaces.Services.State;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.State.RequestGet
{
    public class ResponseBadRequest
    {
        private StatesController _controler;

        [Fact(DisplayName = "E possivel realizar o get")]
        public async Task GetResponse()
        {
            var serviceMock = new Mock<IStateService>();
            var name = Faker.Address.UsState();
            var sigla = Faker.Address.UsState().Substring(1, 3);

            serviceMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(new StateDTO
            {
                Id = Guid.NewGuid(),
                Name = name,
                Sigla = sigla,
            });

            _controler = new StatesController(serviceMock.Object);
            _controler.ModelState.AddModelError("Id", "Formato inválido");

            var result = await _controler.Get(Guid.NewGuid());
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
