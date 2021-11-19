using System;
using System.Collections.Generic;
using Api.Domain.DTOs.State;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class StateMapper : BaseTestService
    {
        [Fact(DisplayName = "É possivel mapear os modelos de estado")]
        public void ModelMappersStates()
        {
            var model = new StateModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Address.UsState(),
                Sigla = Faker.Address.UsState().Substring(1, 3),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
            };

            var entityList = new List<StateModel>();
            for (var i = 0; i < 5; i++)
            {
                var item = new StateModel
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Address.UsState(),
                    Sigla = Faker.Address.UsState().Substring(1, 3),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                };
                entityList.Add(item);
            }


            // model para entity
            var entity = Mapper.Map<StateEntity>(model);
            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Name, entity.Name);
            Assert.Equal(model.Sigla, entity.Sigla);
            Assert.Equal(model.CreateAt, entity.CreateAt);
            Assert.Equal(model.UpdateAt, entity.UpdateAt);

            // model para dto
            var dto = Mapper.Map<StateDTO>(entity);
            Assert.Equal(entity.Id, dto.Id);
            Assert.Equal(entity.Name, dto.Name);
            Assert.Equal(entity.Sigla, dto.Sigla);

            var listDto = Mapper.Map<List<StateDTO>>(entityList);
            Assert.True(entityList.Count == listDto.Count);
            for (var i = 0; i < listDto.Count; i++)
            {
                Assert.Equal(listDto[i].Id, entityList[i].Id);
                Assert.Equal(listDto[i].Name, entityList[i].Name);
                Assert.Equal(listDto[i].Sigla, entityList[i].Sigla);               
            }

            //dto para model
            var stateModel = Mapper.Map<StateModel>(dto);
            Assert.Equal(stateModel.Id, dto.Id);
            Assert.Equal(stateModel.Name, dto.Name);
            Assert.Equal(stateModel.Sigla, dto.Sigla);
            
        }
    }
}
