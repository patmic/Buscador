using System.ComponentModel.DataAnnotations;

namespace SharedApp.Models.Dtos
{
    public class UsuarioAutenticacionDto
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Clave { get; set; }
    }
}