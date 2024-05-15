using System.ComponentModel.DataAnnotations;

namespace ClientApp.Models
{
    public class UsuarioAutenticacion
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string Email { get; set; }        
        [Required(ErrorMessage = "El Clave es obligatorio")]
        public string Clave { get; set; }
    }
}
