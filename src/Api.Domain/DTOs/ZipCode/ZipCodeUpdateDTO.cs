using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTOs.ZipCode
{
    public class ZipCodeUpdateDTO
    {
        [Required(ErrorMessage = "Id é obrigatório")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "CEP é obrigatório")]
        [StringLength(10, ErrorMessage = "CEP deve ter no máximo {1} caracteres")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Endereço é obrigatório")]
        public string Adress { get; set; }

        public string Number { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatório")]
        public Guid CityId { get; set; }
    }
}
