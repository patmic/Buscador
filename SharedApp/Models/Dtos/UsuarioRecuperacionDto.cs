using System.ComponentModel.DataAnnotations;

namespace SharedApp.Models.Dtos
{
    public class UsuarioRecuperacionDto
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string? Email { get; set; }
    }
}