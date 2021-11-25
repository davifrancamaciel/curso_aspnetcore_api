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
    public class ResponseNotFound
    {
        private CitiesController _controler;

        [Fact(DisplayName = "E possivel realizar o get")]
        public async Task GetResponseNotFound()
        {
            var serviceMock = new Mock<ICityService>();
            var name = Faker.Address.City();
            var ibgeId = Faker.RandomNumber.Next(1, 1000);

            serviceMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(Task.FromResult((CityDTO)null));

            _controler = new CitiesController(serviceMock.Object);

            var result = await _controler.Get(Guid.NewGuid());
            Assert.True(result is NotFoundResult);
        }
    }
}
