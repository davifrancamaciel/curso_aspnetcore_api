using System;
using System.Collections.Generic;
using Api.Domain.DTOs.ZipCode;
using Api.Domain.DTOs.State;
using Api.Domain.DTOs.City;

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
        public ZipCodeDTO zipCodeDto;
        public ZipCodeCreateDTO zipCodeCreateDto;
        public ZipCodeCreateResultDTO zipCodeCreateResultDto;
        public ZipCodeUpdateDTO zipCodeUpdateDto;
        public ZipCodeUpdateResultDTO zipCodeUpdateResultDto;

        public ZipCodeTests()
        {
            Id = Guid.NewGuid();
            ZipCode = Faker.Address.ZipCode();
            Adress = Faker.Address.StreetName();
            Number = Faker.RandomNumber.Next(1, 1000).ToString();
            CityId = Guid.NewGuid();

            ZipCodeUpdate = Faker.Address.ZipCode();
            AdressUpdate = Faker.Address.StreetName();
            NumberUpdate = Faker.RandomNumber.Next(1, 1000).ToString();


            for (var i = 0; i < 10; i++)
            {
                var dto = new ZipCodeDTO
                {
                    Id = Guid.NewGuid(),
                    ZipCode = Faker.Address.ZipCode(),
                    Adress = Faker.Address.StreetName(),
                    Number = Faker.RandomNumber.Next(1, 1000).ToString(),
                    CityId = Guid.NewGuid(),
                    City = new CityCompleteDTO
                    {
                        Id = Id,
                        IbgeId = Faker.RandomNumber.Next(1, 1000),
                        Name = Faker.Address.City(),
                        StateId = Guid.NewGuid(),
                        State = new StateDTO
                        {
                            Id = Guid.NewGuid(),
                            Sigla = Faker.Address.UsState().Substring(1, 3),
                            Name = Faker.Address.UsState()
                        }
                    }
                };
                listDto.Add(dto);
            }

            zipCodeDto = new ZipCodeDTO
            {
                Id = Id,
                ZipCode = ZipCode,
                Adress = Adress,
                Number = Number,
                CityId = CityId,
                City = new CityCompleteDTO
                {
                    Id = CityId,
                    IbgeId = Faker.RandomNumber.Next(1, 1000),
                    Name = Faker.Address.City(),
                    StateId = Guid.NewGuid(),
                    State = new StateDTO
                    {
                        Id = Guid.NewGuid(),
                        Sigla = Faker.Address.UsState().Substring(1, 3),
                        Name = Faker.Address.UsState()
                    }
                }
            };



            zipCodeCreateDto = new ZipCodeCreateDTO
            {
                ZipCode = ZipCode,
                Adress = Adress,
                Number = Number,
                CityId = CityId,
            };

            zipCodeCreateResultDto = new ZipCodeCreateResultDTO
            {
                Id = Id,
                ZipCode = ZipCode,
                Adress = Adress,
                Number = Number,
                CityId = CityId,
                CreateAt = DateTime.UtcNow
            };
            zipCodeUpdateDto = new ZipCodeUpdateDTO
            {
                Id = Id,
                ZipCode = ZipCodeUpdate,
                Adress = AdressUpdate,
                Number = NumberUpdate,
                CityId = CityId,
            };

            zipCodeUpdateResultDto = new ZipCodeUpdateResultDTO
            {
                Id = Id,
                ZipCode = ZipCodeUpdate,
                Adress = AdressUpdate,
                Number = NumberUpdate,
                CityId = CityId,
                UpdateAt = DateTime.UtcNow
            };
        }

    }
}
