using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class IVwHomologacion
    {
        [Key]
        public int IdHomologacion { get; set; }
        [Required]
        public string? MostrarWeb { get; set; }
        public string? Descripcion { get; set; }
    }
}