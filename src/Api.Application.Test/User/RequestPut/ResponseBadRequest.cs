using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.Request
{
    public class ResponseBadRequest
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
            _controler
                .ModelState
                .AddModelError("Name", "É um campo obrigatório");

            var userDtoUpdate =
                new UserUpdateDTO {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email
                };

            var result = await _controler.Put(userDtoUpdate);
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_controler.ModelState.IsValid);
        }
    }
}
