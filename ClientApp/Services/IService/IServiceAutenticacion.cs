using ClientApp.Models;

namespace ClientApp.Services.IService
{
    public interface IServiceAutenticacion
    {
        Task<RespuestaAutenticacion> Acceder(UsuarioAutenticacion usuarioDesdeAutenticacion);
        Task Salir();
    }
}
