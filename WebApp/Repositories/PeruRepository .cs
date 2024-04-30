
using System.Data;
using WebApp.Models;
using WebApp.Repositories.IRepositories;
using WebApp.Service;

namespace WebApp.Repositories
{
    public class PeruRepository : IPeruRepository
    {
        private readonly MySqlDbContext _bd;

        public PeruRepository(MySqlDbContext dbContext)
        {
            _bd = dbContext;
        }
        public Organizacion GetOrganizacion(int organizacionId)
        {
            return _bd.Organizacion.FirstOrDefault(c => c.IdOrganizacion == organizacionId);
        }

        public ICollection<Organizacion> GetOrganizaciones()
        {
            return _bd.Organizacion.OrderBy(c => c.IdOrganizacion).ToList();
        }
    }
}