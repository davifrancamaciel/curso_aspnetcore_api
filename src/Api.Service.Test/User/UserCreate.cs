using System;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
    public class UserCreate : UserTests
    {
        private IUserService _service;

        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método de criar usuário")]
        public async Task CreateUsers()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.Post(userCreateDto)).ReturnsAsync(userCreateResultDto);
            _service = _serviceMock.Object;

            var result = await _service.Post(userCreateDto);

            Assert.NotNull (result);
            Assert.True(result.Id != Guid.Empty);
            Assert.Equal(Name, result.Name);
            Assert.Equal(Email, result.Email);            
        }
    }
}
