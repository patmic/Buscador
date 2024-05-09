using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using WebApp.Service;

namespace WebApp.Repositories
{
    public class VwHomologacionRepository : IVwHomologacionRepository
    {
        private readonly SqlServerDbContext _bd;

        public VwHomologacionRepository(SqlServerDbContext dbContext)
        {
            _bd = dbContext;
        }

        public ICollection<VwTipoAcreditacion> ObtenerTipoAcreditacion()
        {
            return _bd.VwTipoAcreditacion.OrderBy(c => c.MostrarWeb).ToList();
        }

        public ICollection<VwEsqAcredita> ObtenerEsqAcredita()
        {
            return _bd.VwEsqAcredita.OrderBy(c => c.MostrarWeb).ToList();
        }

        public ICollection<VwEstado> ObtenerEstado()
        {
            return _bd.VwEstado.OrderBy(c => c.MostrarWeb).ToList();
        }

        public ICollection<VwOrgAcredita> ObtenerOrgAcredita()
        {
            return _bd.VwOrgAcredita.OrderBy(c => c.MostrarWeb).ToList();
        }

        public ICollection<VwPais> ObtenerPais()
        {
            return _bd.VwPais.OrderBy(c => c.MostrarWeb).ToList();
        }

        public ICollection<VwDimension> ObtenerDimension()
        {
            return _bd.VwDimension.OrderBy(c => c.MostrarWeb).ToList();
        }
    }
}