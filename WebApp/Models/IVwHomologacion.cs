using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class IVwHomologacion
    {
        [Key]
        public int IdHomologacion { get; set; }
        [Required]
        public string? BusquedaEtiqueta { get; set; }
    }
}