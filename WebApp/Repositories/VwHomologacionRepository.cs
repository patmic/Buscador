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
        public ICollection<VwDimension> ObtenerDimension()
        {
            return _bd.VwDimension.OrderBy(c => c.MostrarWebOrden).ToList();
        }
        public ICollection<IVwHomologacion> ObtenerFiltroDetalles(int IdHomologacion)
        {
            return _bd.Database.SqlQuery<IVwHomologacion>($"SELECT * FROM fnFiltroDetalle({IdHomologacion})").ToList();
        }
    }
}