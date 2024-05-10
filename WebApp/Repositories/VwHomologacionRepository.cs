using Microsoft.EntityFrameworkCore;
using WebApp.Models;
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
        public ICollection<VwGrilla> ObtenerEtiquetaGrilla()
        {
            return _bd.VwGrilla.OrderBy(c => c.IdHomologacion).ToList();
        }

        public ICollection<VwFiltro> ObtenerEtiquetaFiltros()
        {
            return _bd.VwFiltro.OrderBy(c => c.IdHomologacion).ToList();
        }

        public ICollection<IVwHomologacion> ObtenerFiltroDetalles(int IdHomologacion)
        {
            return _bd.Set<IVwHomologacion>().FromSqlRaw("select *  from fnFiltroDetalle({0})", IdHomologacion).ToList();
        }
    }
}