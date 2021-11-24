using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.State;
using Api.Domain.Interfaces.Services.State;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.State.RequestGetAll
{
    public class ResponseBadRequest
    {
        private StatesController _controler;

        [Fact(DisplayName = "E possivel realizar o get all")]
        public async Task GetResponse()
        {
            var serviceMock = new Mock<IStateService>();

            serviceMock.Setup(x => x.GetAll()).ReturnsAsync(new List<StateDTO> {
                    new StateDTO {
                        Id = Guid.NewGuid(),
                        Name = Faker.Address.UsState(),
                        Sigla=Faker.Address.UsState().Substring(1, 3),
                    },
                    new StateDTO {
                        Id = Guid.NewGuid(),
                        Name = Faker.Address.UsState(),
                        Sigla=Faker.Address.UsState().Substring(1, 3),

                    }
                });

            _controler = new StatesController(serviceMock.Object);
            _controler.ModelState.AddModelError("Id", "Formato inválido");

            var result = await _controler.GetAll();
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
