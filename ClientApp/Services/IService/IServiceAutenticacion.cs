using ClientApp.Models;

namespace ClientApp.Services.IService
{
    public interface IServiceAutenticacion
    {
        Task<RespuestaRegistro> RegistrarUsuario(UsuarioRegistro usuarioParaRegistro);
        Task<RespuestaAutenticacion> Acceder(UsuarioAutenticacion usuarioDesdeAutenticacion);
        Task Salir();
    }
}
