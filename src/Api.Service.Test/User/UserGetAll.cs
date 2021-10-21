using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
    public class UserGetAll : UserTests
    {
        private IUserService _service;

        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método get all de usuários")]
        public async Task GetAllUsers()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.GetAll()).ReturnsAsync(userDtoList);
            _service = _serviceMock.Object;

            var result = await _service.GetAll();

            Assert.NotNull (result);
            Assert.True(result.Any());

            var _listResult = new List<UserDTO>();
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(x => x.GetAll()).ReturnsAsync(_listResult.AsEnumerable);
            _service = _serviceMock.Object;

            var resultEmpty = await _service.GetAll();
            Assert.Empty (resultEmpty);
            Assert.False(resultEmpty.Any());
        }
    }
}
