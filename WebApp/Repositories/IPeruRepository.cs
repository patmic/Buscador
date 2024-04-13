using WebApp.Models;

namespace WebApp.Repositories;
public interface IPeruRepository
{
    Task<IEnumerable<Organizacion>> GetOrganizacionAsync();
    Task<Organizacion> GetOrganizacionByIdAsync(int id);
    // Task<Empresa> AddEmpresaAsync(Empresa p);
    // Task UpdateEmpresaAsync(Empresa p);
    // Task DeleteEmpresaAsync(int Id);
}