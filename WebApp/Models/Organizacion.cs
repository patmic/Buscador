using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;
public class Organizacion
{
    // ORGANIZACION: id,nombre,direccion,telefono,email
    public int id { get; set; }
    [Required]
    public string? nombre { get; set; }
    [Required]
    public string? direccion { get; set; }
    public string? Telefono { get; set; }
    public string? email { get; set; }
}