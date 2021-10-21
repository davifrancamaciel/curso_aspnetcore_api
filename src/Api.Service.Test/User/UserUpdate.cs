using System;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
    public class UserUpdate : UserTests
    {
        private IUserService _service;

        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método de alterar usuário")]
        public async Task UpdateUsers()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.Post(userCreateDto)).ReturnsAsync(userCreateResultDto);
            _service = _serviceMock.Object;

            var resultCreate = await _service.Post(userCreateDto);

            Assert.NotNull (resultCreate);
            Assert.True(resultCreate.Id != Guid.Empty);
            Assert.Equal(Name, resultCreate.Name);
            Assert.Equal(Email, resultCreate.Email);    

            _serviceMock = new Mock<IUserService>();
            _serviceMock
                .Setup(x => x.Put(userUpdateDto))
                .ReturnsAsync(userUpdateResultDto);
            _service = _serviceMock.Object;

            var resultUpdate = await _service.Put(userUpdateDto);

            Assert.NotNull (resultUpdate);
            Assert.Equal(NameUpdated, resultUpdate.Name);
            Assert.Equal(EmailUpdated, resultUpdate.Email);
        }
    }
}
