using System;

namespace Api.Domain.DTOs.City
{
    public class CityDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }        
        public int IbgeId { get; set; }        
        public Guid StateId { get; set; }
    }
}
