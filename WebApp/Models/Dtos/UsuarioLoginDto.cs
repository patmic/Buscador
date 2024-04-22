using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Dtos
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "El email es obligatorio")]
        public string Email { get; set; }       
    
        [Required(ErrorMessage = "La clave es obligatorio")]
        public string Clave { get; set; }
    }
}
