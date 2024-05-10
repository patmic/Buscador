using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    [Table("Homologacion")]
    public class Homologacion
    {
        [Key]
        public int IdHomologacion { get; set; }
        [Required]
        public int IdHomologacionGrupo { get; set; }
        [Required]
        public string ClaveBuscar { get; set; }
        [Required]
        public string Homologado { get; set; }
        [Required]
        public string MostrarWeb { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string InfoExtraJson { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        public int IdUserCrea { get; set; }
        public int IdUserModifica { get; set; }
    }
}
