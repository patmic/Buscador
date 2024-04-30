// modelo de datos para representar la estructura de los datos que se recibirá del API REST
public class Organizacion
{
    public int IdOrganizacion { get; set; }
    public string CodigoAcreditacion { get; set; }
    public string RazonSocial { get; set; }
    public string AreaAcreditacion { get; set; }
    public string Actividad { get; set; }
    public string Ciudad { get; set; }
    public string Estado { get; set; }
}