using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    [Table("Homologacion")]
    public class Homologacion : BaseEntity
    {
        [Key]
        public int IdHomologacion { get; set; }
        [Required]
        public int IdHomologacionGrupo { get; set; }
        [Required]
        public string ClaveBuscar { get; set; }
        [Required]
        public string MostrarWeb { get; set; }
        public int MostrarWebOrden { get; set; }
        [Required]
        public string TooltipWeb { get; set; }
        [Required]
        public string InfoExtraJson { get; set; }
    }
}
