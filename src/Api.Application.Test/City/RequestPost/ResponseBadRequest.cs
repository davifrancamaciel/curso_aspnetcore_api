using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.City;
using Api.Domain.Interfaces.Services.City;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.City.RequestPost
{
    public class ResponseBadRequest
    {
        private CitiesController _controler;

        [Fact(DisplayName = "E possivel realizar o created")]
        public async Task CreateResponse()
        {
            var serviceMock = new Mock<ICityService>();
            var name = Faker.Address.City();
            var ibgeId = Faker.RandomNumber.Next(1, 1000);

            serviceMock.Setup(x => x.Post(It.IsAny<CityCreateDTO>())).ReturnsAsync(new CityCreateResultDTO
            {
                Id = Guid.NewGuid(),
                Name = name,
                IbgeId = ibgeId,
                StateId = Guid.NewGuid(),
                CreateAt = DateTime.UtcNow
            });

            _controler = new CitiesController(serviceMock.Object);
            _controler.ModelState.AddModelError("Name", "É um campo obrigatório");

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controler.Url = url.Object;

            var CityDtoCreate = new CityCreateDTO
            {
                Name = name,
                IbgeId = ibgeId,
                StateId = Guid.NewGuid()
            };

            var result = await _controler.Post(CityDtoCreate);
            Assert.True(result is BadRequestObjectResult);

        }
    }
}
