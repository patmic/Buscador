using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Endpoint
    {
        [Key]
        public int IdEndpoint { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Url { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaModifica { get; set; }
        public int IdUserCrea { get; set; }
        public int IdUserModifica { get; set; }
    }
}