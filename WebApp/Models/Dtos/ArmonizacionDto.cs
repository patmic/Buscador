namespace WebApp.Models.Dtos
{
    public class ArmonizacionDto
    {
        public int IdEtiqueta { get; set; }
        public string? Nombre { get; set; }
        public string? Etiqueta { get; set; }
        public string? Campo { get; set; }
        public string? Comentario { get; set; }
        public string? Tipo { get; set; }
        public int Orden { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        public int IdUserCrea { get; set; }
        public int IdUserModifica { get; set; }
    }
}
