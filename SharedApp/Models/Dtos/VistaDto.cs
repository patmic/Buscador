using System.ComponentModel.DataAnnotations;

namespace SharedApp.Models.Dtos
{
    public class VistaDto
    {
        public int IdVista { get; set; }
        [Required]
        public int IdHomologacionSistema { get; set; }
        [Required]
        public string? VistaNombre { get; set; }
    }
}