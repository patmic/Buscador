using System.ComponentModel.DataAnnotations;

namespace ClientApp.Models
{
    public class UsuarioRecuperacion
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string Email { get; set; }
    }
}
