
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;
public class EcuadorSAEvwBusqueda
{
    // vwBusqueda 
    public int AreaId { get; set; }
    [Required]
    public string? Acreditacion { get; set; }
    public string? AcreditacionActividad { get; set; }
    public string? Ubicacion { get; set; }
    public string? UbicacionTipo { get; set; }
    public string? Estado { get; set; }
    public string? CodigoAcreditacion { get; set; }
    public string? RazonSocial { get; set; }
}