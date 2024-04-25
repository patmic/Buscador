using WebApp.Models;

namespace WebApp.Repositories.IRepositories {
    public interface IPeruRepository
    {
        Organizacion GetOrganizacion(int organizacionId);
        ICollection<Organizacion> GetOrganizaciones();
        // Task<IEnumerable<Organizacion>> GetOrganizacionAsync();
        // Task<Organizacion> GetOrganizacionByIdAsync(int id);
        // Task<Empresa> AddEmpresaAsync(Empresa p);
        // Task UpdateEmpresaAsync(Empresa p);
        // Task DeleteEmpresaAsync(int Id);
    }
}