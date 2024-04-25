
using WebApp.Models;

namespace WebApp.Repositories.IRepositories {
    public interface IEcuadorSAEvwBusquedaRepository
    {
        Task<IEnumerable<EcuadorSAEvwBusqueda>> GetALlAsync_SAEvwBusqueda();
        Task<EcuadorSAEvwBusqueda> GetByIdAsync_SAEvwBusqueda(int id);
    }
}