using WebApp.Models;
using WebApp.Models.Dtos;

namespace WebApp.Repositories.IRepositories
{
    public interface IUsuarioRepository
    {
        Usuario find(int usuarioId);
        bool create(Usuario usuario);
        bool update(Usuario usuario);
        bool isUniqueUser(string usuario);
        ICollection<Usuario> findAll();
        Task<object> login(UsuarioLoginDto usuarioLoginDto);
        Task<object> Recuperar(UsuarioDto usuarioDto);
    }
}
