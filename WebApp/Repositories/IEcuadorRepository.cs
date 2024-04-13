using WebApp.Models;

namespace WebApp.Repositories;
public interface IEcuadorRepository
{
    Task<IEnumerable<Empresa>> GetEmpresasAsync();
    Task<Empresa> GetEmpresasByIdAsync(int id);
    // Task<Empresa> AddEmpresaAsync(Empresa p);
    // Task UpdateEmpresaAsync(Empresa p);
    // Task DeleteEmpresaAsync(int Id);
}