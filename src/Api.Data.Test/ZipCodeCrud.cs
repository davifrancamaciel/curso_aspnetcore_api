using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class ZipCodeCrud : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;

        public ZipCodeCrud(DbTest dbTest)
        {
            _serviceProvider = dbTest.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de cep")]
        [Trait("CRUD", "ZipCodeEntity")]
        public async Task E_Possivel_Realizar_CRUD()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                //post city
                var _cityRepository = new CityImplementation(context);
                var _cityEntity = new CityEntity()
                {
                    Name = Faker.Address.City(),
                    IbgeId = Faker.RandomNumber.Next(1000000, 9999999),
                    StateId = Guid.Parse("43a0f783-a042-4c46-8688-5dd4489d2ec7")
                };
                var _registerCityCreated = await _cityRepository.InsertAsync(_cityEntity);
                Assert.NotNull(_registerCityCreated);
                Assert.Equal(_cityEntity.IbgeId, _registerCityCreated.IbgeId);
                Assert.Equal(_cityEntity.Name, _registerCityCreated.Name);
                Assert.False(_registerCityCreated.Id == Guid.Empty);

                //post zipcode
                var _repository = new ZipCodeImplementation(context);
                var _entity = new ZipCodeEntity()
                {
                    ZipCode = "25.615-071",
                    Adress = Faker.Address.StreetName(),
                    Number = "320 casa B",
                    CityId = _registerCityCreated.Id
                };

                var _registerCreated = await _repository.InsertAsync(_entity);
                Assert.NotNull(_registerCreated);
                Assert.Equal(_entity.ZipCode, _registerCreated.ZipCode);
                Assert.Equal(_entity.Adress, _registerCreated.Adress);
                Assert.Equal(_entity.Number, _registerCreated.Number);
                Assert.Equal(_entity.CityId, _registerCreated.CityId);
                Assert.False(_registerCreated.Id == Guid.Empty);

                //put
                _entity.Adress = Faker.Address.StreetName();
                _entity.ZipCode = "25.615-080";
                _entity.Id = _registerCreated.Id;
                var _registerUpdated = await _repository.UpdateAsync(_entity);
                Assert.NotNull(_registerUpdated);
                Assert.Equal(_entity.Adress, _registerUpdated.Adress);
                Assert.Equal(_entity.ZipCode, _registerUpdated.ZipCode);

                // exists
                var _registerIsExist = await _repository.ExistsAsync(_registerUpdated.Id);
                Assert.True(_registerIsExist);

                //get by id
                var _registerSelected = await _repository.SelectAsync(_registerUpdated.Id);
                Assert.NotNull(_registerSelected);
                Assert.Equal(_registerUpdated.Adress, _registerSelected.Adress);
                Assert.Equal(_registerUpdated.ZipCode, _registerSelected.ZipCode);
                Assert.Equal(_registerUpdated.Number, _registerSelected.Number);
                Assert.Equal(_registerUpdated.CityId, _registerSelected.CityId);

                //get by zipcode
                _registerSelected = await _repository.SelectAsync(_registerUpdated.ZipCode);
                Assert.NotNull(_registerSelected);
                Assert.Equal(_registerUpdated.Adress, _registerSelected.Adress);
                Assert.Equal(_registerUpdated.ZipCode, _registerSelected.ZipCode);
                Assert.Equal(_registerUpdated.Number, _registerSelected.Number);
                Assert.Equal(_registerUpdated.CityId, _registerSelected.CityId);
                Assert.NotNull(_registerUpdated.City);
                Assert.NotNull(_registerUpdated.City.State);

                var _allRegisters = await _repository.SelectAsync();
                Assert.NotNull(_allRegisters);
                Assert.True(_allRegisters.Any());

                var _registerRemoved = await _repository.DeleteAsync(_registerUpdated.Id);
                Assert.True(_registerRemoved);
            }
        }
    }
}
