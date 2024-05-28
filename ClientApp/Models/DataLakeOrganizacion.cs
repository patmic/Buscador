namespace ClientApp.Models
{
    public class ResultDataLakeOrganizacion
    {
        public int IdDataLakeOrganizacion { get; set; }
        public string DataJson { get; set; }
    }
    public class DataLakeOrganizacion
    {
        public int IdDataLakeOrganizacion { get; set; }
        public List<Columna> DataJson { get; set; }
    }
    public class Columna
    {
        public int IdHomologacion { get; set; }
        public string ValorColumna { get; set; }
    }
}