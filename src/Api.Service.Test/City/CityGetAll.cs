using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.DTOs.City;
using Api.Domain.Interfaces.Services.City;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using Xunit;

namespace Api.Service.Test.City
{
    public class CityGetAll : CityTests
    {
        private ICityService _service;

        private Mock<ICityService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método GetAll")]
        public async Task GetAllCity()
        {
            _serviceMock = new Mock<ICityService>();
            _serviceMock.Setup(x => x.GetAll()).ReturnsAsync(listDto);
            _service = _serviceMock.Object;

            var result = await _service.GetAll();

            Assert.NotNull(result);
            Assert.True(result.Any());
            Assert.True(result.Count() == 10);


            var _listResult = new List<CityDTO>();
            _serviceMock = new Mock<ICityService>();
            _serviceMock.Setup(x => x.GetAll()).ReturnsAsync(_listResult.AsEnumerable);
            _service = _serviceMock.Object;

            var _resultEmpty = await _service.GetAll();
            Assert.Empty(_resultEmpty);
            Assert.True(_resultEmpty.Count() == 0);
        }
    }
}
