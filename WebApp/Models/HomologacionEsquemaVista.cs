using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class HomologacionEsquemaVista : BaseEntity
    {
        [Key]
        public int IdHomologacionEsquemaVista { get; set; }
        [Required]
        public int IdHomologacionEsquema { get; set; }
        [Required]
        public int IdVista { get; set; }
        [Required]
        public int IdHomologacion { get; set; }
        [Required]
        public string? VistaColumna { get; set; }
    }
}