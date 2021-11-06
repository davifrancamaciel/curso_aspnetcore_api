using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public class CityEntity : BaseEntity
    {
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [Required]
        public int IbgeId { get; set; }

        [Required]
        public Guid StateId { get; set; }

        public StateEntity State { get; set; }

        public IEnumerable<ZipCodeEntity> ZipCodes { get; set; }
    }
}
