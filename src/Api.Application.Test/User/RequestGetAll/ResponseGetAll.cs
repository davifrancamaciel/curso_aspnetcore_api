using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.RequestGetAll
{
    public class ResponseGet
    {
        private UsersController _controler;

        [Fact(DisplayName = "E possivel realizar o get")]
        public async Task GetResponse()
        {
            var serviceMock = new Mock<IUserService>();

            serviceMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(new List<UserDTO> {
                    new UserDTO {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        CreateAt = DateTime.UtcNow
                    },
                    new UserDTO {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        CreateAt = DateTime.UtcNow
                    }
                });

            _controler = new UsersController(serviceMock.Object);

            var result = await _controler.GetAll();
            Assert.True(result is OkObjectResult);

            var resultValue =
                ((OkObjectResult) result).Value as IEnumerable<UserDTO>;
            Assert.NotNull (resultValue);
            Assert.True(resultValue.Count() == 2);
        }
    }
}
