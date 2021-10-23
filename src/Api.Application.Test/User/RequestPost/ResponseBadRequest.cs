using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.RequestPost
{
    public class ResponseBadRequest
    {
        private UsersController _controler;

        [Fact(DisplayName = "E possivel realizar o created")]
        public async Task CreateResponse()
        {
            var serviceMock = new Mock<IUserService>();
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock
                .Setup(x => x.Post(It.IsAny<UserCreateDTO>()))
                .ReturnsAsync(new UserCreateResultDTO {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    CreateAt = DateTime.UtcNow
                });

            _controler = new UsersController(serviceMock.Object);
            _controler
                .ModelState
                .AddModelError("Name", "É um campo obrigatório");

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url
                .Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns("http://localhost:5000");
            _controler.Url = url.Object;

            var userDtoCreate =
                new UserCreateDTO { Name = name, Email = email };

            var result = await _controler.Post(userDtoCreate);
            Assert.True(result is BadRequestObjectResult);

        }
    }
}
