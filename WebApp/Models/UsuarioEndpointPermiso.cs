using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class UsuarioEndpointPermiso
    {
        [Key]
        public int IdUsuarioEndpointPermiso { get; set; }
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        public int IdEndpoint { get; set; }
        [Required]
        public string Accion { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaModifica { get; set; }
        public int IdUserCrea { get; set; }
        public int IdUserModifica { get; set; }

        // Propiedades de navegación
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }

        [ForeignKey("IdEndpoint")]
        public Endpoint Endpoint { get; set; }
    }
}
