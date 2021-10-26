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
    public class ResponseDeleted
    {
        private UsersController _controler;

        [Fact(DisplayName = "E possivel realizar o delete")]
        public async Task DeleteResponse()
        {
            var serviceMock = new Mock<IUserService>();

            serviceMock
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            _controler = new UsersController(serviceMock.Object);

            var result = await _controler.Delete(Guid.NewGuid());
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult) result).Value;
            Assert.NotNull (resultValue);
            Assert.True((Boolean) resultValue);
        }
    }
}
