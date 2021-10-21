using System;
using System.Collections.Generic;
using Api.Domain.DTOs.User;

namespace Api.Service.Test.User
{
    public class UserTests
    {
        public static string Name { get; set; }

        public static string Email { get; set; }

        public static string NameUpdated { get; set; }

        public static string EmailUpdated { get; set; }

        public static Guid UserId { get; set; }

        public List<UserDTO> userDtoList = new List<UserDTO>();

        public UserDTO userDto;

        public UserCreateDTO userCreateDto;

        public UserCreateResultDTO userCreateResultDto;

        public UserUpdateDTO userUpdateDto = new UserUpdateDTO();

        public UserUpdateResultDTO userUpdateResultDto;

        public UserTests()
        {
            UserId = Guid.NewGuid();
            Name = Faker.Name.FullName();
            Email = Faker.Internet.Email();
            NameUpdated = Faker.Name.FullName();
            EmailUpdated = Faker.Internet.Email();

            for (var i = 0; i < 10; i++)
            {
                var dto =
                    new UserDTO()
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email()
                    };
                userDtoList.Add (dto);
            }

            userDto = new UserDTO() { Id = UserId, Name = Name, Email = Email };
            userCreateDto = new UserCreateDTO() { Name = Name, Email = Email };
            userCreateResultDto =
                new UserCreateResultDTO()
                {
                    Id = UserId,
                    Name = Name,
                    Email = Email,
                    CreateAt = DateTime.UtcNow
                };

            userUpdateDto =
                new UserUpdateDTO()
                { Id = UserId, Name = NameUpdated, Email = EmailUpdated };
            userUpdateResultDto =
                new UserUpdateResultDTO()
                {
                    Id = UserId,
                    Name = NameUpdated,
                    Email = EmailUpdated,
                    UpdateAt = DateTime.UtcNow
                };
        }
    }
}
