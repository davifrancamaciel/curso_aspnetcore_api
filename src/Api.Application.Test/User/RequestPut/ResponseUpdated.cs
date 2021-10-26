using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.RequestPut
{
    public class ResponseUpdated
    {
        private UsersController _controler;

        [Fact(DisplayName = "E possivel realizar o update")]
        public async Task UpdateResponse()
        {
            var serviceMock = new Mock<IUserService>();
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock
                .Setup(x => x.Put(It.IsAny<UserUpdateDTO>()))
                .ReturnsAsync(new UserUpdateResultDTO {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    UpdateAt = DateTime.UtcNow
                });

            _controler = new UsersController(serviceMock.Object);

            var userDtoUpdate =
                new UserUpdateDTO {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email
                };

            var result = await _controler.Put(userDtoUpdate);
            Assert.True(result is OkObjectResult);

            var resultValue =
                ((OkObjectResult) result).Value as UserUpdateResultDTO;

            Assert.NotNull (resultValue);
            Assert.Equal(userDtoUpdate.Name, resultValue.Name);
            Assert.Equal(userDtoUpdate.Email, resultValue.Email);
            Assert.True(resultValue.Id != Guid.Empty);
        }
    }
}
