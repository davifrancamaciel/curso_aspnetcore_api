using System;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class CityCrud : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;

        public CityCrud(DbTest dbTest)
        {
            _serviceProvider = dbTest.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de cidade")]
        [Trait("CRUD", "CityEntity")]
        public async Task E_Possivel_Realizar_CRUD()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                var _repository = new CityImplementation(context);
                var _entity = new CityEntity()
                {
                    Name = Faker.Address.City(),
                    IbgeId = Faker.RandomNumber.Next(1000000, 9999999),
                    StateId = Guid.Parse("43a0f783-a042-4c46-8688-5dd4489d2ec7")
                };

                //post
                var _registerCreated = await _repository.InsertAsync(_entity);
                Assert.NotNull(_registerCreated);
                Assert.Equal(_entity.IbgeId, _registerCreated.IbgeId);
                Assert.Equal(_entity.Name, _registerCreated.Name);
                Assert.False(_registerCreated.Id == Guid.Empty);

                //put
                _entity.Name = Faker.Address.City();
                _entity.Id = _registerCreated.Id;
                var _registerUpdated = await _repository.UpdateAsync(_entity);
                Assert.NotNull(_registerUpdated);
                Assert.Equal(_entity.IbgeId, _registerUpdated.IbgeId);
                Assert.Equal(_entity.Name, _registerUpdated.Name);
                Assert.Equal(_entity.StateId, _registerUpdated.StateId);

                // exists
                var _registerIsExist = await _repository.ExistsAsync(_registerUpdated.Id);
                Assert.True(_registerIsExist);

                //get by id
                var _registerSelected = await _repository.SelectAsync(_registerUpdated.Id);
                Assert.NotNull(_registerSelected);
                Assert.Equal(_registerUpdated.IbgeId, _registerSelected.IbgeId);
                Assert.Equal(_registerUpdated.Name, _registerSelected.Name);
                Assert.Equal(_registerUpdated.StateId, _registerSelected.StateId);
                Assert.Null(_registerUpdated.State);

                //get complete by id ibge
                _registerSelected = await _repository.GetCompleteByIBGE(_registerUpdated.IbgeId);
                Assert.NotNull(_registerSelected);
                Assert.Equal(_registerUpdated.IbgeId, _registerSelected.IbgeId);
                Assert.Equal(_registerUpdated.Name, _registerSelected.Name);
                Assert.Equal(_registerUpdated.StateId, _registerSelected.StateId);
                Assert.NotNull(_registerUpdated.State);

                //get complete by id ibge
                _registerSelected = await _repository.GetCompleteById(_registerUpdated.Id);
                Assert.NotNull(_registerSelected);
                Assert.Equal(_registerUpdated.IbgeId, _registerSelected.IbgeId);
                Assert.Equal(_registerUpdated.Name, _registerSelected.Name);
                Assert.Equal(_registerUpdated.StateId, _registerSelected.StateId);
                Assert.NotNull(_registerUpdated.State);


                var _allRegisters = await _repository.SelectAsync();
                Assert.NotNull(_allRegisters);
                Assert.True(_allRegisters.Any());

                var _registerRemoved = await _repository.DeleteAsync(_registerUpdated.Id);
                Assert.True(_registerRemoved);
            }
        }
    }
}
