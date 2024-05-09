using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;
public class DataLakeOrganizacion
{
    [Key]
    public int IdDataLakeOrganizacion { get; set; }
    public int? IdHomologacionSistema { get; set; }
    public string? DataId { get; set; }
    public string? DataJson { get; set; }
    public string? DataJsonExtra { get; set; }
    public string? DataJsonEstado { get; set; }
}