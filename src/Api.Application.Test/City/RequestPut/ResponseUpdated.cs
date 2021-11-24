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
    public class ResponseUpdated
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

            var CityDtoUpdate = new CityUpdateDTO
            {
                Id = Guid.NewGuid(),
                Name = name,
                IbgeId = ibgeId,
                StateId = Guid.NewGuid(),
            };

            var result = await _controler.Put(CityDtoUpdate);
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as CityUpdateResultDTO;

            Assert.NotNull(resultValue);
            Assert.Equal(CityDtoUpdate.Name, resultValue.Name);
            Assert.Equal(CityDtoUpdate.IbgeId, resultValue.IbgeId);
            Assert.True(resultValue.Id != Guid.Empty);
        }
    }
}
