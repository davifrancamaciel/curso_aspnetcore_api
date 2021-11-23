
using System;
using System.Threading.Tasks;
using Api.Domain.DTOs.ZipCode;
using Api.Domain.Interfaces.Services.ZipCode;
using Moq;
using Xunit;

namespace Api.Service.Test.ZipCode
{
    public class ZipCodeUpdate : ZipCodeTests
    {
        private IZipCodeService _service;

        private Mock<IZipCodeService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método de alterar cep")]
        public async Task UpdateCeps()
        {
            _serviceMock = new Mock<IZipCodeService>();
            _serviceMock.Setup(x => x.Put(zipCodeUpdateDto)).ReturnsAsync(zipCodeUpdateResultDto);
            _service = _serviceMock.Object;

            var resultUpdate = await _service.Put(zipCodeUpdateDto);

            Assert.NotNull(resultUpdate);
            Assert.True(resultUpdate.Id != Guid.Empty);
            Assert.Equal(ZipCodeUpdate, resultUpdate.ZipCode);
            Assert.Equal(AdressUpdate, resultUpdate.Adress);
            Assert.Equal(NumberUpdate, resultUpdate.Number);
        }
    }
}
