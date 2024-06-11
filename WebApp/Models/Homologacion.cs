using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharedApp.Models;

namespace WebApp.Models
{
    [Table("Homologacion")]
    public class Homologacion : BaseEntity
    {
        [Key]
        public int IdHomologacion { get; set; }
        public int? IdHomologacionGrupo { get; set; }
        [Required]
        public string? MostrarWeb { get; set; }
        public int MostrarWebOrden { get; set; }
        public string? TooltipWeb { get; set; }
        public string? InfoExtraJson { get; set; }
        public string? MascaraDato { get; set; }
        public string? SiNoHayDato { get; set; }
        public string? CodigoHomologacion { get; set; }
        public string? NombreHomologado { get; set; }
    }
}
