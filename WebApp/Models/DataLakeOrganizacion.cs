using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;
public class DataLakeOrganizacion
{
    [Key]
    public int IdDataLakeOrganizacion { get; set; }
    public int? IdHomologacionSistema { get; set; }
    public int? IdDataLake { get; set; }
    public string? DataEsquemaJson { get; set; }
    public string? Estado { get; set; }
    public bool? Activo { get; set; }
}