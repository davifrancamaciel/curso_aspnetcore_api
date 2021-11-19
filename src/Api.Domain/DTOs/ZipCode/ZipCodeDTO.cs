using System;
using Api.Domain.DTOs.City;

namespace Api.Domain.DTOs.ZipCode
{
    public class ZipCodeDTO
    {
        public Guid Id { get; set; }

        public string ZipCode { get; set; }

        public string Adress { get; set; }

        public string Number { get; set; }

        public Guid CityId { get; set; }

        public CityCompleteDTO City { get; set; }
    }
}
