namespace WebApp.Models
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            Estado = "A";
            FechaModifica = DateTime.Now;
        }

        public string Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModifica { get; set; }
        public int IdUserCreacion { get; set; }
        public int IdUserModifica { get; set; }
    }
}