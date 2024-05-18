using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    [Table("vwDimension")]
    public class VwDimension : IVwHomologacion
    {
        public int MostrarWebOrden { get; set; }
    }
}