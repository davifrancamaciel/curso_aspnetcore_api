using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.City;
using Api.Domain.DTOs.State;
using Api.Domain.Interfaces.Services.City;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.City.RequestGetCompleteByIbge
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

            serviceMock.Setup(x => x.GetCompleteByIBGE(It.IsAny<int>())).ReturnsAsync(new CityCompleteDTO
            {
                Id = Guid.NewGuid(),
                Name = name,
                IbgeId = ibgeId,
                StateId = Guid.NewGuid(),
                State = new StateDTO
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Address.UsState(),
                    Sigla = Faker.Address.UsState().Substring(1, 3),
                }
            });

            _controler = new CitiesController(serviceMock.Object);

            var result = await _controler.GetCompleteByIBGE(It.IsAny<int>());
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value as CityDTO;
            Assert.NotNull(resultValue);
            Assert.Equal(name, resultValue.Name);
            Assert.Equal(ibgeId, resultValue.IbgeId);
        }
    }
}
