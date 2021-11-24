using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.DTOs.State;
using Api.Domain.Interfaces.Services.State;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.State.RequestGet
{
    public class ResponseNotFound
    {
        private StatesController _controler;

        [Fact(DisplayName = "E possivel realizar o get")]
        public async Task GetResponse()
        {
            var serviceMock = new Mock<IStateService>();
           
            serviceMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(Task.FromResult((StateDTO)null));

            _controler = new StatesController(serviceMock.Object);
            var result = await _controler.Get(Guid.NewGuid());
            Assert.True(result is NotFoundResult);
        }
    }
}
