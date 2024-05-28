// Utilizaremos el patrón Repository para esto.
using ClientApp.Models;

namespace ClientApp.Services.IService {
    public interface IBusquedaRepository
    {
        Task<List<DataLakeOrganizacion>> BuscarPalabraAsync(string campo, params int[] fields);
        Task<List<HomologacionEsquema>> ObtenerEsquemasRelacionados(int Id);
    }
}