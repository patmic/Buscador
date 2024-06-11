using SharedApp.Models;
using SharedApp.Models.Dtos;

namespace ClientApp.Services.IService
{
    public interface IServiceAutenticacion
    {
        Task<RespuestasAPI> Acceder(UsuarioAutenticacionDto usuarioAutenticacionDto);
        Task<RespuestasAPI> Recuperar(UsuarioRecuperacionDto usuarioRecuperacionDto);
        Task Salir();
    }
}
