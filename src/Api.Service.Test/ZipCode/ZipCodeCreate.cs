
using System;
using System.Threading.Tasks;
using Api.Domain.DTOs.ZipCode;
using Api.Domain.Interfaces.Services.ZipCode;
using Moq;
using Xunit;

namespace Api.Service.Test.ZipCode
{
    public class ZipCodeCreate : ZipCodeTests
    {
        private IZipCodeService _service;

        private Mock<IZipCodeService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método de criar cep")]
        public async Task CreateCeps()
        {
            _serviceMock = new Mock<IZipCodeService>();
            _serviceMock.Setup(x => x.Post(zipCodeCreateDto)).ReturnsAsync(zipCodeCreateResultDto);
            _service = _serviceMock.Object;

            var result = await _service.Post(zipCodeCreateDto);

            Assert.NotNull(result);
            Assert.True(result.Id != Guid.Empty);
            Assert.Equal(ZipCode, result.ZipCode);
            Assert.Equal(Adress, result.Adress);
            Assert.Equal(Number, result.Number);
        }
    }
}
