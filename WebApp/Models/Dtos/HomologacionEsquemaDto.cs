namespace WebApp.Models.Dtos
{
  public class HomologacionEsquemaDto
  {
    public int IdHomologacionEsquema { get; set; }
    public string? CodigoHomologacionEsquema { get; set; }
    public string? NombreHomologacionEsquema { get; set; }
    public string? HomologacionEsquemaJson { get; set; }
    public int MostrarWebOrden { get; set; }
    public string? MostrarWeb { get; set; }
    public string? Descripcion { get; set; }
    public DateTime? FechaCrea { get; set; }
    public DateTime? FechaModifica { get; set; }
    public int? IdUserCrea { get; set; }
    public int? IdUserModifica { get; set; }
  }
}
