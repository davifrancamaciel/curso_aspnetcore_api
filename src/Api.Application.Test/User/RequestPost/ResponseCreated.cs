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
    public class ResponseCreated
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

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url
                .Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns("http://localhost:5000");
            _controler.Url = url.Object;

            var userDtoCreate =
                new UserCreateDTO { Name = name, Email = email };

            var result = await _controler.Post(userDtoCreate);
            Assert.True(result is CreatedResult);

            var resultValue =
                ((CreatedResult) result).Value as UserCreateResultDTO;

            Assert.NotNull (resultValue);
            Assert.Equal(userDtoCreate.Name, resultValue.Name);
            Assert.Equal(userDtoCreate.Email, resultValue.Email);
            Assert.True(resultValue.Id != Guid.Empty);
        }
    }
}
