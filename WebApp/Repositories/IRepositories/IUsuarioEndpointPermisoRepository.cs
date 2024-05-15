using WebApp.Models;
using WebApp.Models.Dtos;

namespace WebApp.Repositories.IRepositories {
    public interface IUsuarioEndpointPermisoRepository
    {
        ICollection<UsuarioEndpointPermiso> GetUsuarioEndpointPermisos();
        UsuarioEndpointPermiso GetUsuarioEndpointPermiso(int endpointId);
        Task<UsuarioEndpointPermiso> Registro(UsuarioEndpointPermisoRegistroDto usuarioEndpointPermisoRegistroDto);
    }
}