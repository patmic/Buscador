using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;
public class Empresa
{
    public int EmpresaID { get; set; }
    [Required]
    public string? Nombre { get; set; }
    [Required]
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
}