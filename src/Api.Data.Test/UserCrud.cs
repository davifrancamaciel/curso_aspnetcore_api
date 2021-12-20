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
    public class UserCrud : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;

        public UserCrud(DbTest dbTest)
        {
            _serviceProvider = dbTest.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de usuário")]
        [Trait("CRUD", "UserEntity")]
        public async Task E_Possivel_Realizar_CRUD()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                var _repository = new UserImplementation(context);
                var _entity =
                    new UserEntity()
                    {
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email()
                    };
                var _registerCreated = await _repository.InsertAsync(_entity);
                Assert.NotNull(_registerCreated);
                Assert.Equal(_entity.Email, _registerCreated.Email);
                Assert.Equal(_entity.Name, _registerCreated.Name);
                Assert.False(_registerCreated.Id == Guid.Empty);

                _entity.Name = Faker.Name.First();
                var _registerUpdated = await _repository.UpdateAsync(_entity);
                Assert.NotNull(_registerUpdated);
                Assert.Equal(_entity.Email, _registerUpdated.Email);
                Assert.Equal(_entity.Name, _registerUpdated.Name);

                var _registerIsExist = await _repository.ExistsAsync(_registerUpdated.Id);
                Assert.True(_registerIsExist);

                var _registerSelected =
                    await _repository.SelectAsync(_registerUpdated.Id);
                Assert.NotNull(_registerIsExist);
                Assert.Equal(_registerUpdated.Email, _registerSelected.Email);
                Assert.Equal(_registerUpdated.Name, _registerSelected.Name);

                var _allRegisters = await _repository.SelectAsync();
                Assert.NotNull(_allRegisters);
                Assert.True(_allRegisters.Any());

                var _userAuthenticatted =
                    await _repository.FindByLogin(_registerSelected.Email);
                Assert.NotNull(_userAuthenticatted);
                Assert
                    .Equal(_registerSelected.Email, _userAuthenticatted.Email);
                Assert.Equal(_registerSelected.Name, _userAuthenticatted.Name);

                var _registerRemoved =
                    await _repository.DeleteAsync(_registerUpdated.Id);
                Assert.True(_registerRemoved);
            }
        }
    }
}
