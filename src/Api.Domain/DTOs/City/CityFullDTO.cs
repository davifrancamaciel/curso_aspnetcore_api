using System;
using Api.Domain.DTOs.State;

namespace Api.Domain.DTOs.City
{
    public class CityFullDTO : CityDTO
    {
        public StateDTO State { get; set; }
    }
}
