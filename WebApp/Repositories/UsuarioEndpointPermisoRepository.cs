
using System.Data;
using WebApp.Repositories.IRepositories;
using WebApp.Models.Dtos;
using WebApp.Service;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class UsuarioEndpointPermisoRepository : IUsuarioEndpointPermisoRepository
    {
        private readonly SqlServerDbContext _bd;

        public UsuarioEndpointPermisoRepository(SqlServerDbContext dbContext)
        {
            _bd = dbContext;
        }

        public UsuarioEndpointPermiso GetUsuarioEndpointPermiso(int endpointId)
        {
            return _bd.UsuarioEndpointPermiso.FirstOrDefault(c => c.IdEndpoint == endpointId);
        }

        public ICollection<UsuarioEndpointPermiso> GetUsuarioEndpointPermisos()
        {
            return _bd.UsuarioEndpointPermiso.OrderBy(c => c.IdUsuarioEndpointPermiso).ToList();
        }

        public async Task<UsuarioEndpointPermiso> Registro(UsuarioEndpointPermisoRegistroDto usuarioEndpointPermisoRegistroDto)
        {
            UsuarioEndpointPermiso usuarioEndpointPermiso = new UsuarioEndpointPermiso()
            {
                IdUsuario = usuarioEndpointPermisoRegistroDto.IdUsuario,
                IdEndpoint = usuarioEndpointPermisoRegistroDto.IdEndpoint,
                Accion = usuarioEndpointPermisoRegistroDto.Accion,
                FechaCreacion = DateTime.Now,
                FechaModifica = DateTime.Now
            };

            _bd.UsuarioEndpointPermiso.Add(usuarioEndpointPermiso);
            await _bd.SaveChangesAsync();
            return usuarioEndpointPermiso;
        }

        public bool IsUniqueUser(string nombre, string url)
        {
            var endpointbd = _bd.Endpoint.FirstOrDefault(u => u.Nombre == nombre && u.Url == url);
            if (endpointbd == null)
            {
                return true;
            }

            return false;
        }
    }
}