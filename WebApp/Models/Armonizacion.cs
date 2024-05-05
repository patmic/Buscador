using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Armonizacion
    {
        [Key]
        public int IdEtiqueta { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Etiqueta { get; set; }
        [Required]
        public string Campo { get; set; }
        [Required]
        public string Comentario { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public int Orden { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        public int IdUserCrea { get; set; }
        public int IdUserModifica { get; set; }
    }
}
