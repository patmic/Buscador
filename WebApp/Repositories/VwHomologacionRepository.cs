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

        public ICollection<VwAlcance> ObtenerAlcance()
        {
            return _bd.VwAlcance.OrderBy(c => c.BusquedaEtiqueta).ToList();
        }

        public ICollection<VwEsqAcredita> ObtenerEsqAcredita()
        {
            return _bd.VwEsqAcredita.OrderBy(c => c.BusquedaEtiqueta).ToList();
        }

        public ICollection<VwEstado> ObtenerEstado()
        {
            return _bd.VwEstado.OrderBy(c => c.BusquedaEtiqueta).ToList();
        }

        public ICollection<VwOrgAcredita> ObtenerOrgAcredita()
        {
            return _bd.VwOrgAcredita.OrderBy(c => c.BusquedaEtiqueta).ToList();
        }

        public ICollection<VwPais> ObtenerPais()
        {
            return _bd.VwPais.OrderBy(c => c.BusquedaEtiqueta).ToList();
        }

        public ICollection<VwRazonSocial> ObtenerRazonSocial()
        {
            return _bd.VwRazonSocial.OrderBy(c => c.BusquedaEtiqueta).ToList();
        }
    }
}