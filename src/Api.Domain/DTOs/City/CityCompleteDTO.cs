using System;
using Api.Domain.DTOs.State;

namespace Api.Domain.DTOs.City
{
    public class CityCompleteDTO : CityDTO
    {
        public StateDTO State { get; set; }
    }
}
