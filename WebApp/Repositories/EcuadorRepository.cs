using WebApp.Models;
using WebApp.Repositories.IRepositories;
using WebApp.Service;

namespace WebApp.Repositories
{
    public class EcuadorRepository : IEcuadorRepository
    {
        private readonly SqlServerDbContext _bd;

        public EcuadorRepository(SqlServerDbContext dbContext)
        {
            _bd = dbContext;
        }

        public Empresa GetEmpresa(int empresaId)
        {
            return _bd.Empresa.FirstOrDefault(c => c.EmpresaID == empresaId);
        }

        public ICollection<Empresa> GetEmpresas()
        {
            return _bd.Empresa.OrderBy(c => c.EmpresaID).ToList();
        }
    }
}