using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTOs.City
{
    public class CityCreateDTO
    {
        [Required(ErrorMessage = "Nome da cidade é obrigatório")]
        [
            StringLength(
                60,
                ErrorMessage =
                    "Nome da cidade deve ter no máximo {1} caracteres")
        ]
        public string Name { get; set; }

        [Required(ErrorMessage = "Código do IBGE é obrigatório")]
        [Range(0, int.MaxValue)]
        public int IbgeId { get; set; }

        [Required(ErrorMessage = "Código do estado é obrigatório")]
        public Guid StateId { get; set; }
    }
}
