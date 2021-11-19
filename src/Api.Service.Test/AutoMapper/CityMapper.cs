using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.DTOs.City;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class CityMapper : BaseTestService
    {
        [Fact(DisplayName = "É possivel mapear os modelos de cidades")]
        public void ModelMappersCities()
        {
            var model = new CityModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Address.City(),
                StateId = Guid.NewGuid(),
                IbgeId = Faker.RandomNumber.Next(1, 100),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
            };



            var entityList = new List<CityEntity>();
            for (var i = 0; i < 5; i++)
            {
                var item = new CityEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Address.City(),
                    StateId = Guid.NewGuid(),
                    IbgeId = Faker.RandomNumber.Next(1, 100),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    State = new StateEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Address.UsState(),
                        Sigla = Faker.Address.UsState().Substring(1, 3),
                        CreateAt = DateTime.UtcNow,
                        UpdateAt = DateTime.UtcNow,
                    }
                };
                entityList.Add(item);
            };

            // model para entity
            var entity = Mapper.Map<CityEntity>(model);
            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Name, entity.Name);
            Assert.Equal(model.IbgeId, entity.IbgeId);
            Assert.Equal(model.StateId, entity.StateId);
            Assert.Equal(model.CreateAt, entity.CreateAt);
            Assert.Equal(model.UpdateAt, entity.UpdateAt);

            // entity para dto
            var dto = Mapper.Map<CityDTO>(entity);
            Assert.Equal(entity.Id, dto.Id);
            Assert.Equal(entity.Name, dto.Name);
            Assert.Equal(entity.IbgeId, dto.IbgeId);
            Assert.Equal(entity.StateId, dto.StateId);

            var dtoComplete = Mapper.Map<CityCompleteDTO>(entityList.FirstOrDefault());
            Assert.Equal(entityList.FirstOrDefault().Id, dtoComplete.Id);
            Assert.Equal(entityList.FirstOrDefault().Name, dtoComplete.Name);
            Assert.Equal(entityList.FirstOrDefault().IbgeId, dtoComplete.IbgeId);
            Assert.Equal(entityList.FirstOrDefault().StateId, dtoComplete.StateId);
            Assert.NotNull(dtoComplete.State);

            var listDto = Mapper.Map<List<CityEntity>>(entityList);
            Assert.True(entityList.Count == listDto.Count);
            for (var i = 0; i < listDto.Count; i++)
            {
                Assert.Equal(listDto[i].Id, entityList[i].Id);
                Assert.Equal(listDto[i].Name, entityList[i].Name);
                Assert.Equal(listDto[i].IbgeId, entityList[i].IbgeId);
                Assert.Equal(listDto[i].StateId, entityList[i].StateId);
            }

            var dtoCreateResult = Mapper.Map<CityCreateResultDTO>(entity);
            Assert.Equal(dtoCreateResult.Id, entity.Id);
            Assert.Equal(dtoCreateResult.Name, entity.Name);
            Assert.Equal(dtoCreateResult.IbgeId, entity.IbgeId);
            Assert.Equal(dtoCreateResult.StateId, entity.StateId);
            Assert.Equal(dtoCreateResult.CreateAt, entity.CreateAt);

            var dtoUpdateResult = Mapper.Map<CityUpdateResultDTO>(entity);
            Assert.Equal(dtoUpdateResult.Id, entity.Id);
            Assert.Equal(dtoUpdateResult.Name, entity.Name);
            Assert.Equal(dtoUpdateResult.IbgeId, entity.IbgeId);
            Assert.Equal(dtoUpdateResult.StateId, entity.StateId);
            Assert.Equal(dtoUpdateResult.UpdateAt, entity.UpdateAt);

            //dto para model
            var cityModel = Mapper.Map<CityModel>(dto);
            Assert.Equal(cityModel.Id, dto.Id);
            Assert.Equal(cityModel.Name, dto.Name);
            Assert.Equal(cityModel.IbgeId, dto.IbgeId);
            Assert.Equal(cityModel.StateId, dto.StateId);

            var dtoCreate = Mapper.Map<CityCreateDTO>(cityModel);
            Assert.Equal(dtoCreate.Name, cityModel.Name);
            Assert.Equal(dtoCreate.IbgeId, cityModel.IbgeId);
            Assert.Equal(dtoCreate.StateId, cityModel.StateId);

            var dtoUpdate = Mapper.Map<CityUpdateDTO>(cityModel);
            Assert.Equal(dtoUpdate.Id, cityModel.Id);
            Assert.Equal(dtoUpdate.Name, cityModel.Name);
            Assert.Equal(dtoUpdate.IbgeId, cityModel.IbgeId);
            Assert.Equal(dtoUpdate.StateId, cityModel.StateId);

        }
    }
}
