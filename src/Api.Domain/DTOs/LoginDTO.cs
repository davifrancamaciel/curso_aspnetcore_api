using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email é obrigatório para login")]
        [EmailAddress(ErrorMessage = "Email em formato inválido")]
        [StringLength(100, ErrorMessage = "Email deve ter no máximo {1} caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória para login")]
        public string Password { get; set; }
    }
}
