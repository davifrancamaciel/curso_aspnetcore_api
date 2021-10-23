using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UserMapper : BaseTestService
    {
        [Fact(DisplayName = "É possivel mapear os modelos")]
        public async Task ModelMappersUsers()
        {
            var model =
                new UserModel {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };

            var entityList = new List<UserEntity>();
            for (var i = 0; i < 5; i++)
            {
                var item =
                    new UserEntity {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        CreateAt = DateTime.UtcNow,
                        UpdateAt = DateTime.UtcNow
                    };
                entityList.Add (item);
            }

            // model para entity
            var entity = Mapper.Map<UserEntity>(model);
            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Name, entity.Name);
            Assert.Equal(model.Email, entity.Email);
            Assert.Equal(model.CreateAt, entity.CreateAt);
            Assert.Equal(model.UpdateAt, entity.UpdateAt);

            // model para dto
            var userDto = Mapper.Map<UserDTO>(entity);
            Assert.Equal(entity.Id, userDto.Id);
            Assert.Equal(entity.Name, userDto.Name);
            Assert.Equal(entity.Email, userDto.Email);
            Assert.Equal(entity.CreateAt, userDto.CreateAt);

            var listDto = Mapper.Map<List<UserDTO>>(entityList);
            Assert.True(entityList.Count == listDto.Count);
            for (var i = 0; i < listDto.Count; i++)
            {
                Assert.Equal(listDto[i].Id, entityList[i].Id);
                Assert.Equal(listDto[i].Name, entityList[i].Name);
                Assert.Equal(listDto[i].Email, entityList[i].Email);
                Assert.Equal(listDto[i].CreateAt, entityList[i].CreateAt);
            }

            var userDtoCreateResult = Mapper.Map<UserCreateResultDTO>(entity);
            Assert.Equal(userDtoCreateResult.Id, entity.Id);
            Assert.Equal(userDtoCreateResult.Name, entity.Name);
            Assert.Equal(userDtoCreateResult.Email, entity.Email);
            Assert.Equal(userDtoCreateResult.CreateAt, entity.CreateAt);

            var userDtoUpdateResult = Mapper.Map<UserUpdateResultDTO>(entity);
            Assert.Equal(userDtoUpdateResult.Id, entity.Id);
            Assert.Equal(userDtoUpdateResult.Name, entity.Name);
            Assert.Equal(userDtoUpdateResult.Email, entity.Email);
            Assert.Equal(userDtoUpdateResult.CreateAt, entity.CreateAt);
            Assert.Equal(userDtoUpdateResult.UpdateAt, entity.UpdateAt);

            //dto para model
            var userModel = Mapper.Map<UserModel>(userDto);
            Assert.Equal(userModel.Id, userDto.Id);
            Assert.Equal(userModel.Name, userDto.Name);
            Assert.Equal(userModel.Email, userDto.Email);
            Assert.Equal(userModel.CreateAt, userDto.CreateAt);

            var userDtoCreate = Mapper.Map<UserCreateDTO>(userModel);
            Assert.Equal(userDtoCreate.Name, userModel.Name);
            Assert.Equal(userDtoCreate.Email, userModel.Email);

            var userDtoUpdate = Mapper.Map<UserUpdateDTO>(userModel);
            Assert.Equal(userDtoUpdate.Id, userModel.Id);
            Assert.Equal(userDtoUpdate.Name, userModel.Name);
            Assert.Equal(userDtoUpdate.Email, userModel.Email);
        }
    }
}
