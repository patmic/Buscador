using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
[Table("psBuscarPalabra")]
  public class BuscadorOrganizacion
  {
    [Key]
    public int IdDataLakeOrganizacion { get; set; }
    // public int? IdHomologacionSistema { get; set; }
    public string? DataJson { get; set; }
  }
}
