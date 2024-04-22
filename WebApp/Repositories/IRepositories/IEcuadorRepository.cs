using WebApp.Models;

namespace WebApp.Repositories.IRepositories {
    public interface IEcuadorRepository
    {
        Empresa GetEmpresa(int empresaId);
        public ICollection<Empresa> GetEmpresas();
        // Task<IEnumerable<Empresa>> GetEmpresasAsync();
        // Task<Empresa> GetEmpresasByIdAsync(int id);
        // Task<Empresa> AddEmpresaAsync(Empresa p);
        // Task UpdateEmpresaAsync(Empresa p);
        // Task DeleteEmpresaAsync(int Id);
    }
}