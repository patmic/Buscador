using SharedApp.Models.Dtos;
using WebApp.Models;

namespace WebApp.Repositories.IRepositories
{
    public interface IUsuarioRepository
    {
        Usuario find(int usuarioId);
        bool create(Usuario usuario);
        bool update(Usuario usuario);
        bool isUniqueUser(string usuario);
        ICollection<Usuario> findAll();
        Task<UsuarioAutenticacionRespuestaDto> login(UsuarioAutenticacionDto usuarioAutenticacionDto);
        Task<bool> Recuperar(UsuarioRecuperacionDto usuarioRecuperacionDto);
    }
}
