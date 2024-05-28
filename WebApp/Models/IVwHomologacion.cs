using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class IVwHomologacion
    {
        [Key]
        public int IdHomologacion { get; set; }
        [Required]
        public string? MostrarWeb { get; set; }
        public string? TooltipWeb { get; set; }
        public int MostrarWebOrden { get; set; }
    }
}