namespace WebApp.Models.Dtos
{
    public class CatalogosDto
    {
        public int IdHomologacion { get; set; } 
        public string? MostrarWeb { get; set; }
        public string? TooltipWeb { get; set; }
        public int MostrarWebOrden { get; set; }
        public string? MascaraDato { get; set; }
        public string? SiNoHayDato { get; set; }
        public string? NombreHomologado { get; set; }
        public string? CustomMostrarWeb { get; set; }
    }
}
