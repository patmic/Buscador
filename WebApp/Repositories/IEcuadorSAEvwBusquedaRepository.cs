
using WebApp.Models;

namespace WebApp.Repositories;
public interface IEcuadorSAEvwBusquedaRepository
{
    Task<IEnumerable<EcuadorSAEvwBusqueda>> GetALlAsync_SAEvwBusqueda();
    Task<EcuadorSAEvwBusqueda> GetByIdAsync_SAEvwBusqueda(int id);
}