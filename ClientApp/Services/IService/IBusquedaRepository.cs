// Utilizaremos el patrón Repository para esto.
namespace ClientApp.Services.IService {
    public interface IBusquedaRepository
    {
        Task<List<ResultadoBusqueda>> BuscarPalabraAsync(string campo, params int[] fields);
    }
}