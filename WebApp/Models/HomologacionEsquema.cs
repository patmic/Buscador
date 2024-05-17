using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
  [Table("HomologacionEsquema")]
  public class HomologacionEsquema
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdHomologacionEsquema { get; set; }
    [Required]
    public string CodigoHomologacionEsquema { get; set; }
    [Required]
    public string NombreHomologacionEsquema { get; set; }
    [Required]
    public string HomologacionEsquemaJson { get; set; }
    [Required]
    public int MostrarWebOrden { get; set; }
    [Required]
    public string MostrarWeb { get; set; }
    public string? Descripcion { get; set; }
    public DateTime? FechaCrea { get; set; }
    public DateTime? FechaModifica { get; set; }
    public int? IdUserCrea { get; set; }
    public int? IdUserModifica { get; set; }
  }
}
