using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.City;
using Api.Domain.Interfaces.Services.City;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.City.RequestGet
{
    public class ResponseGet
    {
        private CitiesController _controler;

        [Fact(DisplayName = "E possivel realizar o get")]
        public async Task GetResponse()
        {
            var serviceMock = new Mock<ICityService>();
            var name = Faker.Address.City();
            var ibgeId = Faker.RandomNumber.Next(1, 1000);

            serviceMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(new CityDTO
            {
                Id = Guid.NewGuid(),
                Name = name,
                IbgeId = ibgeId,
                StateId = Guid.NewGuid(),
            });

            _controler = new CitiesController(serviceMock.Object);

            var result = await _controler.Get(Guid.NewGuid());
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as CityDTO;
            Assert.NotNull(resultValue);
            Assert.Equal(name, resultValue.Name);
            Assert.Equal(ibgeId, resultValue.IbgeId);
        }
    }
}
