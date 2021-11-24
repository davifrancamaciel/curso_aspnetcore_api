using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.City;
using Api.Domain.Interfaces.Services.City;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.City.RequestPost
{
    public class ResponseCreated
    {
        private CitiesController _controler;

        [Fact(DisplayName = "E possivel realizar o created")]
        public async Task CreateResponse()
        {
            var serviceMock = new Mock<ICityService>();
            var name = Faker.Address.City();
            var ibgeId = Faker.RandomNumber.Next(1, 1000);
            var stateId = Guid.NewGuid();

            serviceMock.Setup(x => x.Post(It.IsAny<CityCreateDTO>())).ReturnsAsync(new CityCreateResultDTO
            {
                Id = Guid.NewGuid(),
                Name = name,
                IbgeId = ibgeId,
                StateId = stateId,
                CreateAt = DateTime.UtcNow
            });

            _controler = new CitiesController(serviceMock.Object);

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controler.Url = url.Object;

            var CityDtoCreate = new CityCreateDTO
            {
                Name = name,
                IbgeId = ibgeId,
                StateId = stateId
            };

            var result = await _controler.Post(CityDtoCreate);
            Assert.True(result is CreatedResult);

            var resultValue = ((CreatedResult)result).Value as CityCreateResultDTO;

            Assert.NotNull(resultValue);
            Assert.Equal(CityDtoCreate.Name, resultValue.Name);
            Assert.Equal(CityDtoCreate.IbgeId, resultValue.IbgeId);
            Assert.Equal(CityDtoCreate.StateId, resultValue.StateId);
            Assert.True(resultValue.Id != Guid.Empty);
        }
    }
}
