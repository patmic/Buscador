using System.ComponentModel.DataAnnotations;

namespace ClientApp.Models
{
  public class HomologacionEsquema
  {
    public int IdHomologacionEsquema { get; set; }
    [Required]
    public string EsquemaJson { get; set; }
    [Required]
    public int MostrarWebOrden { get; set; }
    [Required]
    public string MostrarWeb { get; set; }
    [Required]
    public string TooltipWeb { get; set; }
  }
}
