using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.City;
using Api.Domain.Interfaces.Services.City;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.City.RequestGetAll
{
    public class ResponseBadRequest
    {
        private CitiesController _controler;

        [Fact(DisplayName = "E possivel realizar o get all")]
        public async Task GetResponse()
        {
            var serviceMock = new Mock<ICityService>();

            serviceMock.Setup(x => x.GetAll()).ReturnsAsync(new List<CityDTO> {
                    new CityDTO {
                        Id = Guid.NewGuid(),
                        Name = Faker.Address.City(),
                        IbgeId = Faker.RandomNumber.Next(1, 1000),
                        StateId = Guid.NewGuid(),
                    },
                    new CityDTO {
                       Id = Guid.NewGuid(),
                        Name = Faker.Address.City(),
                        IbgeId = Faker.RandomNumber.Next(1, 1000),
                        StateId = Guid.NewGuid(),
                    }
                });

            _controler = new CitiesController(serviceMock.Object);
            _controler.ModelState.AddModelError("Id", "Formato inválido");

            var result = await _controler.GetAll();
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
