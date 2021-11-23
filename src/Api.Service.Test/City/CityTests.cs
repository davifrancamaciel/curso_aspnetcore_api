using System;
using System.Collections.Generic;
using Api.Domain.DTOs.City;
using Api.Domain.DTOs.State;

namespace Api.Service.Test.City
{
    public class CityTests
    {
        public static Guid Id { get; set; }
        public static string Name { get; set; }
        public static string NameUpdate { get; set; }
        public static int IbgeId { get; set; }
        public static int IbgeIdUpdate { get; set; }
        public static Guid StateId { get; set; }


        public List<CityDTO> listDto = new List<CityDTO>();
        public CityDTO cityDto;
        public CityCompleteDTO cityCompleteDto;
        public CityCreateDTO cityCreateDto;
        public CityUpdateResultDTO cityUpdateResultDto;
        public CityUpdateDTO cityUpdateDto;
        public CityCreateResultDTO cityCreateResultDto;

        public CityTests()
        {
            Id = Guid.NewGuid();
            Name = Faker.Address.City();
            NameUpdate = Faker.Address.City();
            IbgeId = Faker.RandomNumber.Next(1, 1000);
            IbgeIdUpdate = Faker.RandomNumber.Next(1, 1000);
            StateId = Guid.NewGuid();


            for (var i = 0; i < 10; i++)
            {
                var dto = new CityDTO
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Address.City(),
                    IbgeId = Faker.RandomNumber.Next(1, 1000),
                    StateId = Guid.NewGuid(),
                };
                listDto.Add(dto);
            }

            cityDto = new CityDTO
            {
                Id = Id,
                IbgeId = IbgeId,
                Name = Name,
                StateId = StateId
            };

            cityCompleteDto = new CityCompleteDTO
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

            cityCreateDto = new CityCreateDTO
            {
                IbgeId = IbgeId,
                Name = Name,
                StateId = StateId
            };

            cityCreateResultDto = new CityCreateResultDTO
            {
                Id = Id,
                IbgeId = IbgeId,
                Name = Name,
                StateId = StateId,
                CreateAt = DateTime.UtcNow
            };
            cityUpdateDto = new CityUpdateDTO
            {
                Id = Id,
                IbgeId = IbgeId,
                Name = Name,
                StateId = StateId
            };

            cityUpdateResultDto = new CityUpdateResultDTO
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
