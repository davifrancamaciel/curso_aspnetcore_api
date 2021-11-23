using System;
using System.Threading.Tasks;
using Api.Domain.DTOs.ZipCode;
using Api.Domain.Interfaces.Services.ZipCode;
using Moq;
using Xunit;

namespace Api.Service.Test.ZipCode
{
    public class ZipCodeDelete : ZipCodeTests
    {
        private IZipCodeService _service;

        private Mock<IZipCodeService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método delete")]
        public async Task DeleteZipCode()
        {
            _serviceMock = new Mock<IZipCodeService>();
            _serviceMock.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(true);
            _service = _serviceMock.Object;

            var result = await _service.Delete(Id);
            Assert.True(result);

            _serviceMock = new Mock<IZipCodeService>();
            _serviceMock.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(false);
            _service = _serviceMock.Object;

            result = await _service.Delete(Guid.NewGuid());
            Assert.False(result);
        }
    }
}
