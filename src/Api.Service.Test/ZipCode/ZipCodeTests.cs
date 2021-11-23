using System;
using System.Collections.Generic;
using Api.Domain.DTOs.ZipCode;
using Api.Domain.DTOs.State;

namespace Api.Service.Test.ZipCode
{
    public class ZipCodeTests
    {
        public static Guid Id { get; set; }
        public static string ZipCode { get; set; }
        public static string ZipCodeUpdate { get; set; }

        public static string Adress { get; set; }
        public static string AdressUpdate { get; set; }

        public static string Number { get; set; }
        public static string NumberUpdate { get; set; }
        
        public static Guid CityId { get; set; }


        public List<ZipCodeDTO> listDto = new List<ZipCodeDTO>();
        public ZipCodeDTO ZipCodeDto;
        public ZipCodeCompleteDTO ZipCodeCompleteDto;
        public ZipCodeCreateDTO ZipCodeCreateDto;
        public ZipCodeUpdateResultDTO ZipCodeUpdateResultDto;
        public ZipCodeUpdateDTO ZipCodeUpdateDto;
        public ZipCodeCreateResultDTO ZipCodeCreateResultDto;

        public ZipCodeTests()
        {
            Id = Guid.NewGuid();
            Name = Faker.Address.ZipCode();
            NameUpdate = Faker.Address.ZipCode();
            IbgeId = Faker.RandomNumber.Next(1, 1000);
            IbgeIdUpdate = Faker.RandomNumber.Next(1, 1000);
            StateId = Guid.NewGuid();


            for (var i = 0; i < 10; i++)
            {
                var dto = new ZipCodeDTO
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Address.ZipCode(),
                    IbgeId = Faker.RandomNumber.Next(1, 1000),
                    StateId = Guid.NewGuid(),
                };
                listDto.Add(dto);
            }

            ZipCodeDto = new ZipCodeDTO
            {
                Id = Id,
                IbgeId = IbgeId,
                Name = Name,
                StateId = StateId
            };

            ZipCodeCompleteDto = new ZipCodeCompleteDTO
            {
                Id = Id,
                IbgeId = IbgeId,
                Name = Name,
                StateId = StateId,
                State = new StateDTO
                {
                    Id = Guid.NewGuid(),
                    Sigla = Faker.Address.UsState().Substring(1, 3),
                    Name = Faker.Address.UsState()
                }
            };

            ZipCodeCreateDto = new ZipCodeCreateDTO
            {
                IbgeId = IbgeId,
                Name = Name,
                StateId = StateId
            };

            ZipCodeCreateResultDto = new ZipCodeCreateResultDTO
            {
                Id = Id,
                IbgeId = IbgeId,
                Name = Name,
                StateId = StateId,
                CreateAt = DateTime.UtcNow
            };
            ZipCodeUpdateDto = new ZipCodeUpdateDTO
            {
                Id = Id,
                IbgeId = IbgeId,
                Name = Name,
                StateId = StateId
            };

            ZipCodeUpdateResultDto = new ZipCodeUpdateResultDTO
            {
                Id = Id,
                IbgeId = IbgeId,
                Name = Name,
                StateId = StateId,
                UpdateAt = DateTime.UtcNow
            };
        }

    }
}
