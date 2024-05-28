using ClientApp.Models;

namespace ClientApp.Services.IService {
    public interface IUsuariosRepository
    {
        Task<List<Usuario>> GetUsuariosAsync();
        Task<Usuario> GetUsuarioAsync(int IdUsuario);
        Task<RespuestaRegistro> RegistrarOActualizar(Usuario usuarioParaRegistro);
    }
}