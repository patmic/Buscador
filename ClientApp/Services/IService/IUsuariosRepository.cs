using ClientApp.Models;
using SharedApp.Models;

namespace ClientApp.Services.IService {
    public interface IUsuariosRepository
    {
        Task<List<UsuarioDto>> GetUsuariosAsync();
        Task<UsuarioDto> GetUsuarioAsync(int IdUsuario);
        Task<RespuestaRegistro> RegistrarOActualizar(UsuarioDto usuarioParaRegistro);
    }
}