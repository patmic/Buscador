
using System.ComponentModel.DataAnnotations;
namespace WebApp.Models;
public class CANOrganizacion
{
    public int IdOrganizacion { get; set; }
    [Required]
    public int IdAcreditacion { get; set; }
    [Required]
    public int IdActividad { get; set; }
    [Required]
    public int IdCiudad { get; set; }
    [Required]
    public string? RazonSocial { get; set; }
    public string? CodigoAcreditacion { get; set; }
    public string? AreaAcreditacion { get; set; }
    public string? Actividad { get; set; }
    public string? Ciudad { get; set; }
    public string? Estado { get; set; }
}