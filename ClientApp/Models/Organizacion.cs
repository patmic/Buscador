// modelo de datos para representar la estructura de los datos que se recibirá del API REST
public class Organizacion
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
}