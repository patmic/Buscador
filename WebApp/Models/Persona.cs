using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class Persona
{
    public int id { get; set; }
    [Required]
    public string? nombre { get; set; }
    [Required]
    public string? email { get; set; }

}
