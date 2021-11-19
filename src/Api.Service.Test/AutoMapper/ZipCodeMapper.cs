using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.DTOs.ZipCode;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class ZipCodeMapper : BaseTestService
    {
        [Fact(DisplayName = "É possivel mapear os modelos de ceps")]
        public void ModelMappersZipCodes()
        {

            var model = new ZipCodeModel
            {
                Id = Guid.NewGuid(),
                ZipCode = Faker.RandomNumber.Next(1, 10000).ToString(),
                Adress = Faker.Address.StreetAddress(),
                Number = string.Empty,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                CityId = Guid.NewGuid()
            };

            var entityList = new List<ZipCodeEntity>();
            for (var i = 0; i < 5; i++)
            {
                var item = new ZipCodeEntity
                {
                    Id = Guid.NewGuid(),
                    ZipCode = Faker.RandomNumber.Next(1, 10000).ToString(),
                    Adress = Faker.Address.StreetAddress(),
                    Number = string.Empty,
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    CityId = Guid.NewGuid(),
                    City = new CityEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Address.City(),
                        StateId = Guid.NewGuid(),
                        IbgeId = Faker.RandomNumber.Next(1, 100),
                        State = new StateEntity
                        {
                            Id = Guid.NewGuid(),
                            Name = Faker.Address.UsState(),
                            Sigla = Faker.Address.UsState().Substring(1, 3),
                        }
                    }
                };
                entityList.Add(item);
            }

            // model para entity
            var entity = Mapper.Map<ZipCodeEntity>(model);
            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.ZipCode, entity.ZipCode);
            Assert.Equal(model.Adress, entity.Adress);
            Assert.Equal(model.Number, entity.Number);
            Assert.Equal(model.CityId, entity.CityId);
            Assert.Equal(model.CreateAt, entity.CreateAt);
            Assert.Equal(model.UpdateAt, entity.UpdateAt);

            // entity para dto
            var dto = Mapper.Map<ZipCodeEntity>(entity);
            Assert.Equal(dto.Id, entity.Id);
            Assert.Equal(dto.ZipCode, entity.ZipCode);
            Assert.Equal(dto.Adress, entity.Adress);
            Assert.Equal(dto.Number, entity.Number);
            Assert.Equal(dto.CityId, entity.CityId);
            Assert.Equal(dto.CreateAt, entity.CreateAt);
            Assert.Equal(dto.UpdateAt, entity.UpdateAt);

            var dtoComplete = Mapper.Map<ZipCodeDTO>(entityList.FirstOrDefault());
            Assert.Equal(entityList.FirstOrDefault().Id, dtoComplete.Id);
            Assert.Equal(entityList.FirstOrDefault().ZipCode, dtoComplete.ZipCode);
            Assert.Equal(entityList.FirstOrDefault().Adress, dtoComplete.Adress);
            Assert.Equal(entityList.FirstOrDefault().Number, dtoComplete.Number);
            Assert.NotNull(dtoComplete.City);
            Assert.NotNull(dtoComplete.City.State);

            var listDto = Mapper.Map<List<ZipCodeDTO>>(entityList);
            Assert.True(entityList.Count == listDto.Count);
            for (var i = 0; i < listDto.Count; i++)
            {
                Assert.Equal(listDto[i].Id, entityList[i].Id);
                Assert.Equal(listDto[i].ZipCode, entityList[i].ZipCode);
                Assert.Equal(listDto[i].Adress, entityList[i].Adress);
                Assert.Equal(listDto[i].Number, entityList[i].Number);
            }

            var dtoCreateResult = Mapper.Map<ZipCodeCreateResultDTO>(entity);
            Assert.Equal(dtoCreateResult.Id, entity.Id);
            Assert.Equal(dtoCreateResult.ZipCode, entity.ZipCode);
            Assert.Equal(dtoCreateResult.Adress, entity.Adress);
            Assert.Equal(dtoCreateResult.Number, entity.Number);
            Assert.Equal(dtoCreateResult.CreateAt, entity.CreateAt);

            var dtoUpdateResult = Mapper.Map<ZipCodeUpdateResultDTO>(entity);
            Assert.Equal(dtoUpdateResult.Id, entity.Id);
            Assert.Equal(dtoUpdateResult.ZipCode, entity.ZipCode);
            Assert.Equal(dtoUpdateResult.Adress, entity.Adress);
            Assert.Equal(dtoUpdateResult.Number, entity.Number);
            Assert.Equal(dtoUpdateResult.UpdateAt, entity.UpdateAt);


            //dto para model
            var zipCodeModel = Mapper.Map<ZipCodeModel>(dto);
            Assert.Equal(zipCodeModel.Id, dto.Id);
            Assert.Equal(zipCodeModel.ZipCode, dto.ZipCode);
            Assert.Equal(zipCodeModel.Adress, dto.Adress);
            Assert.Equal("S/N", dto.Number);

            var dtoCreate = Mapper.Map<ZipCodeCreateDTO>(zipCodeModel);
            Assert.Equal(dtoCreate.ZipCode, zipCodeModel.ZipCode);
            Assert.Equal(dtoCreate.Adress, zipCodeModel.Adress);
            Assert.Equal(dtoCreate.Number, zipCodeModel.Number);

            var dtoUpdate = Mapper.Map<ZipCodeUpdateDTO>(zipCodeModel);
            Assert.Equal(dtoUpdate.Id, zipCodeModel.Id);
            Assert.Equal(dtoUpdate.ZipCode, zipCodeModel.ZipCode);
            Assert.Equal(dtoUpdate.Adress, zipCodeModel.Adress);
            Assert.Equal(dtoUpdate.Number, zipCodeModel.Number);
        }
    }
}
