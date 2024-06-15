using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharedApp.Models;

namespace WebApp.Models
{
    public class Vista : BaseEntity
    {
        [Key]
        public int IdVista { get; set; }
        [Required]
        public int IdHomologacionSistema { get; set; }
        [Required]
        public string? VistaNombre { get; set; }
    }
}
