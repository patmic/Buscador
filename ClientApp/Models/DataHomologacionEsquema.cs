namespace ClientApp.Models
{
    public class DataHomologacionEsquema
    {
        public int IdDataLakeOrganizacion { get; set; }
        public int IdHomologacionEsquema { get; set; }
        public List<ColumnaEsquema> DataEsquemaJson { get; set; }
    }
    public class ColumnaEsquema
    {
        public int IdHomologacion { get; set; }
        public string Data { get; set; }
    }
}