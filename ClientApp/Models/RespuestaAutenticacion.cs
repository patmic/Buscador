namespace ClientApp.Models
{
    public class RespuestaAutenticacion
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public Usuario Usuario { get; set; }
    }
}
