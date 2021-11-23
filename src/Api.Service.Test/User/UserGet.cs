using System;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
    public class UserGet : UserTests
    {
        private IUserService _service;

        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método get")]
        public async Task GetUsers()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.Get(UserId)).ReturnsAsync(userDto);
            _service = _serviceMock.Object;

            var result = await _service.Get(UserId);

            Assert.NotNull(result);
            Assert.True(result.Id == UserId);
            Assert.Equal(Name, result.Name);

            _serviceMock = new Mock<IUserService>();
            _serviceMock
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult((UserDTO)null));
            _service = _serviceMock.Object;

            var _record = await _service.Get(UserId);
            Assert.Null(_record);
        }
    }
}
