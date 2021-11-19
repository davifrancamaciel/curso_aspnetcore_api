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
        [Fact(DisplayName = "É possivel mapear os modelos de usuários")]
        public void ModelMappersUsers()
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
            var dto = Mapper.Map<UserDTO>(entity);
            Assert.Equal(entity.Id, dto.Id);
            Assert.Equal(entity.Name, dto.Name);
            Assert.Equal(entity.Email, dto.Email);
            Assert.Equal(entity.CreateAt, dto.CreateAt);

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
            var userModel = Mapper.Map<UserModel>(dto);
            Assert.Equal(userModel.Id, dto.Id);
            Assert.Equal(userModel.Name, dto.Name);
            Assert.Equal(userModel.Email, dto.Email);
            Assert.Equal(userModel.CreateAt, dto.CreateAt);

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
