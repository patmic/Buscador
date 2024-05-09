namespace WebApp.Models.Dtos
{
  public class DataLakeOrganizacionDto
  {
    public int IdDataLakeOrganizacion { get; set; }
    public int? IdHomologacionSistema { get; set; }
    public string? DataId { get; set; }
    public string? DataJson { get; set; }
    public string? DataJsonExtra { get; set; }
    public string? DataJsonEstado { get; set; }
  }
}
