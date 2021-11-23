using System;
using System.Threading.Tasks;
using Api.Domain.DTOs.ZipCode;
using Api.Domain.Interfaces.Services.ZipCode;
using Moq;
using Xunit;

namespace Api.Service.Test.ZipCode
{
    public class ZipCodeGet : ZipCodeTests
    {
        private IZipCodeService _service;

        private Mock<IZipCodeService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método get")]
        public async Task GetZipCode()
        {
            _serviceMock = new Mock<IZipCodeService>();
            _serviceMock.Setup(x => x.Get(Id)).ReturnsAsync(zipCodeDto);
            _service = _serviceMock.Object;

            var result = await _service.Get(Id);
            Assert.NotNull(result);
            Assert.True(result.Id == Id);
            Assert.Equal(ZipCode, result.ZipCode);
            Assert.Equal(Adress, result.Adress);

            _serviceMock = new Mock<IZipCodeService>();
            _serviceMock.Setup(x => x.Get(ZipCode)).ReturnsAsync(zipCodeDto);
            _service = _serviceMock.Object;

            result = await _service.Get(ZipCode);
            Assert.NotNull(result);
            Assert.True(result.Id == Id);
            Assert.Equal(ZipCode, result.ZipCode);
            Assert.Equal(Adress, result.Adress);

            _serviceMock = new Mock<IZipCodeService>();
            _serviceMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(Task.FromResult((ZipCodeDTO)null));
            _service = _serviceMock.Object;

            var _record = await _service.Get(Id);
            Assert.Null(_record);
        }
    }
}
