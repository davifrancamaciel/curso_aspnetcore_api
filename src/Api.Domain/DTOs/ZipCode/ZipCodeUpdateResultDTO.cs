using System;

namespace Api.Domain.DTOs.ZipCode
{
    public class ZipCodeUpdateResultDTO
    {
        public Guid Id { get; set; }

        public string ZipCode { get; set; }

        public string Adress { get; set; }

        public string Number { get; set; }

        public Guid CityId { get; set; }

        public DateTime UpdateAt { get; set; }
    }
}
