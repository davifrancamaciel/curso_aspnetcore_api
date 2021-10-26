using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.RequestGet
{
    public class ResponseBadRequest
    {
        private UsersController _controler;

        [Fact(DisplayName = "E possivel realizar o get")]
        public async Task GetResponse()
        {
            var serviceMock = new Mock<IUserService>();
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .ReturnsAsync(new UserDTO {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    CreateAt = DateTime.UtcNow
                });

            _controler = new UsersController(serviceMock.Object);
            _controler.ModelState.AddModelError("Id", "Formato inválido");

            var result = await _controler.Get(Guid.NewGuid());
            Assert.True(result is BadRequestObjectResult);
        }
    }
}
