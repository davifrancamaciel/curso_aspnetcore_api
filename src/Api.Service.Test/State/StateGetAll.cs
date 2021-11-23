using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTOs.State;
using Api.Domain.Interfaces.Services.State;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using Xunit;

namespace Api.Service.Test.State
{
    public class StateGetAll : StateTests
    {
        private IStateService _service;

        private Mock<IStateService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método getAll")]
        public async Task GetStates()
        {
            _serviceMock = new Mock<IStateService>();
            _serviceMock.Setup(x => x.GetAll()).ReturnsAsync(listDto);
            _service = _serviceMock.Object;

            var result = await _service.GetAll();

            Assert.NotNull(result);
            Assert.True(result.Any());
            Assert.True(result.Count() == 10);

            var _listResult = new List<StateDTO>();
            _serviceMock = new Mock<IStateService>();
            _serviceMock.Setup(x => x.GetAll()).ReturnsAsync(_listResult.AsEnumerable);
            _service = _serviceMock.Object;

            var _resultEmpty = await _service.GetAll();
            Assert.Empty(_resultEmpty);
            Assert.True(_resultEmpty.Count() == 0);
        }

    }
}
