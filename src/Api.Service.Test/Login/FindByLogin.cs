using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.DTOs.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Models;
using Moq;
using Xunit;

namespace Api.Service.Test.Login
{
    public class FindByLogin : BaseTestService
    {
        private ILoginService _service;

        private Mock<ILoginService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o metodo findByLogin")]
        public async Task FindByLoginUser()
        {
            var email = Faker.Internet.Email();
            var retorno =
                new {
                    authenticated = true,
                    created = DateTime.UtcNow,
                    expiration = DateTime.UtcNow.AddHours(8),
                    accessToken = Guid.NewGuid().ToString(),
                    userName = Faker.Name.FullName(),
                    userEmail = email,
                    MessageProcessingHandler = "Usuário logado com sucesso"
                };

            var loginDto = new LoginDto { Email = email };

            _serviceMock = new Mock<ILoginService>();
            _serviceMock
                .Setup(x => x.FindByLogin(loginDto))
                .ReturnsAsync(retorno);
            _service = _serviceMock.Object;

            var result = await _service.FindByLogin(loginDto);
            Assert.NotNull (result);
        }
    }
}
