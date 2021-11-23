using System;
using System.Collections.Generic;
using Api.Domain.DTOs.State;

namespace Api.Service.Test.State
{
    public class StateTests
    {
        public static Guid Id { get; set; }
        public static string Name { get; set; }
        public static string Sigla { get; set; }

        public List<StateDTO> listDto = new List<StateDTO>();
        public StateDTO stateDto;

        public StateTests()
        {
            Id = Guid.NewGuid();
            Sigla = Faker.Address.UsState().Substring(1, 3);
            Name = Faker.Address.UsState();


            for (var i = 0; i < 10; i++)
            {
                var dto = new StateDTO
                {
                    Id = Guid.NewGuid(),
                    Sigla = Faker.Address.UsState().Substring(1, 3),
                    Name = Faker.Address.UsState()
                };
                listDto.Add(dto);
            }

            stateDto = new StateDTO
            {
                Id = Id,
                Sigla = Sigla,
                Name = Name
            };
        }

    }
}
