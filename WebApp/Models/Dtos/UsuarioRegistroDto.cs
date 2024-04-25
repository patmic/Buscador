using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Dtos
{
    public class UsuarioRegistroDto
    {
        [Required(ErrorMessage = "El email es obligatorio")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El rol es obligatorio")]
        public string Rol { get; set; }
        [Required(ErrorMessage = "La clave es obligatorio")]
        public string Clave { get; set; }
    }
}
