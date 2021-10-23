using System;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
    public class UserDelete : UserTests
    {
        private IUserService _service;

        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método get")]
        public async Task DeleteUsers()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(true);
            _service = _serviceMock.Object;

            var result = await _service.Delete(UserId);

            Assert.True(result);            
        }
    }
}
