using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class StateGets : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;
        public StateGets(DbTest dbTest)
        {
            _serviceProvider = dbTest.ServiceProvider;
        }

        [Fact(DisplayName = "Get de estados")]
        [Trait("GET", "StateEntity")]
        public async Task E_Possivel_Realizar_GET_Estatos()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                var _repository = new StateImplementation(context);
                var _entity = new StateEntity
                {
                    Id = Guid.Parse("43a0f783-a042-4c46-8688-5dd4489d2ec7"),
                    Sigla = "RJ",
                    Name = "Rio de Janeiro",
                };

                var isExist = await _repository.ExistsAsync(_entity.Id);
                Assert.True(isExist);

                var _resgisterSelected = await _repository.SelectAsync(_entity.Id);
                Assert.NotNull(_resgisterSelected);
                Assert.Equal(_resgisterSelected.Sigla, _entity.Sigla);
                Assert.Equal(_resgisterSelected.Name, _entity.Name);


                var _allRegisters = await _repository.SelectAsync();
                Assert.NotNull(_allRegisters);
                Assert.True(_allRegisters.Count() == 27);

            }
        }
    }
}
