using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;
public class DataLake
{
    [Key]
    public int IdDataLake { get; set; }
    public string? DataTipo { get; set; }
    public string? DataSistemaOrigen { get; set; }
    public string? DataSistemaOrigenId { get; set; }
    public DateTime? DataSistemaFecha { get; set; }
    public DateTime? DataFechaCarga { get; set; }
    public string? Estado { get; set; }
}