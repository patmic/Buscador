using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharedApp.Models;

namespace WebApp.Models
{
  [Table("HomologacionEsquema")]
  public class HomologacionEsquema : BaseEntity
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdHomologacionEsquema { get; set; }
    [Required]
    public string? EsquemaJson { get; set; }
    [Required]
    public int MostrarWebOrden { get; set; }
    [Required]
    public string? MostrarWeb { get; set; }
    public string? TooltipWeb { get; set; }
  }
}