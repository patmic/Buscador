using System.ComponentModel.DataAnnotations;

namespace SharedApp.Models.Dtos
{
    public class HomologacionEsquemaVistaDto
    {
        public int IdHomologacionEsquemaVista { get; set; }
        [Required]
        public int IdHomologacionEsquema { get; set; }
        [Required]
        public int IdVista { get; set; }
        [Required]
        public int IdHomologacion { get; set; }
        [Required]
        public string? VistaColumna { get; set; }
        public string? NombreHomologado { get; set; }
    }
}