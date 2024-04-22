using WebApp.Models;

namespace WebApp.Models.Dtos
{
    public class UsuarioLoginRespuestaDto
    {
        public Usuario Usuario { get; set; }
        public string Token {  get; set; }
    }
}
