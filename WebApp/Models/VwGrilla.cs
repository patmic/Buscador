using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    [Table("vwGrilla")]
    public class VwGrilla : IVwHomologacion
    {
        public string? MostarNivel { get; set; }
    }
}