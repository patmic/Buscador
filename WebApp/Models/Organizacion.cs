using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;
public class Organizacion
{
    // ORGANIZACION: id,nombre,direccion,telefono,email
    public int Id { get; set; }
    [Required]
    public string? Nombre { get; set; }
    [Required]
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
}