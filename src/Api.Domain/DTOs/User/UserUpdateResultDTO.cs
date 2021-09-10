using System;

namespace Api.Domain.DTOs.User
{
    public class UserUpdateResultDTO : UserCreateResultDTO
    {
        public DateTime UpdateAt { get; set; }
    }
}
