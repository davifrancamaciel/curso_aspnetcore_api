using System;
using System.Threading.Tasks;
using Api.Domain.DTOs.State;
using Api.Domain.Interfaces.Services.State;
using Moq;
using Xunit;

namespace Api.Service.Test.State
{
    public class StateGet : StateTests
    {
        private IStateService _service;

        private Mock<IStateService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método get")]
        public async Task GetStates()
        {
            _serviceMock = new Mock<IStateService>();
            _serviceMock.Setup(x => x.Get(Id)).ReturnsAsync(stateDto);
            _service = _serviceMock.Object;

            var result = await _service.Get(Id);

            Assert.NotNull(result);
            Assert.True(result.Id == Id);
            Assert.Equal(Name, result.Name);

            _serviceMock = new Mock<IStateService>();
            _serviceMock
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(Task.FromResult((StateDTO)null));
            _service = _serviceMock.Object;

            var _record = await _service.Get(Id);
            Assert.Null(_record);
        }

    }
}
