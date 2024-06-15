namespace ClientApp.Models
{
    public class ResultDataHomologacionEsquema
    {
        public List<DataHomologacionEsquema>? Data { get; set; }
        public int TotalCount { get; set; }
    }
    public class DataHomologacionEsquema
    {
        public int IdDataLakeOrganizacion { get; set; }
        public int IdHomologacionEsquema { get; set; }
        public List<ColumnaEsquema>? DataEsquemaJson { get; set; }
    }
    public class ColumnaEsquema
    {
        public int IdHomologacion { get; set; }
        public string? Data { get; set; }
    }
}