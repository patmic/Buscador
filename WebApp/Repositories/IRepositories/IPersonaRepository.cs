using WebApp.Models;

namespace WebApp.Repositories.IRepositories {
    public interface IPersonaRepository
    {
        Task<IEnumerable<Persona>> GetPersonasAsync();
        Task<Persona> GetPersonasByIdAsync(int id);
        Task<Persona> AddPersonaAsync(Persona p);
        Task UpdatePersonaAsync(Persona p);
        Task DeletePersonaAsync(int Id);
    }
}