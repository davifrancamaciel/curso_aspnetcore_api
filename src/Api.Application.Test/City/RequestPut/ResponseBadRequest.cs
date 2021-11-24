using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.City;
using Api.Domain.Interfaces.Services.City;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.City.Request
{
    public class ResponseBadRequest
    {
        private CitiesController _controler;

        [Fact(DisplayName = "E possivel realizar o update")]
        public async Task UpdateResponse()
        {
            var serviceMock = new Mock<ICityService>();
            var name = Faker.Address.City();
            var ibgeId = Faker.RandomNumber.Next(1, 1000);

            serviceMock.Setup(x => x.Put(It.IsAny<CityUpdateDTO>())).ReturnsAsync(new CityUpdateResultDTO
            {
                Id = Guid.NewGuid(),
                Name = name,
                IbgeId = ibgeId,
                StateId = Guid.NewGuid(),
                UpdateAt = DateTime.UtcNow
            });

            _controler = new CitiesController(serviceMock.Object);
            _controler.ModelState.AddModelError("Name", "É um campo obrigatório");

            var CityDtoUpdate = new CityUpdateDTO
            {
                Id = Guid.NewGuid(),
                Name = name,
                IbgeId = ibgeId,
                StateId = Guid.NewGuid(),
            };

            var result = await _controler.Put(CityDtoUpdate);
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controler.ModelState.IsValid);
        }
    }
}
