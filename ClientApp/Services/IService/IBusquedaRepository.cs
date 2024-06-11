// Utilizaremos el patrón Repository para esto.
using ClientApp.Models;

namespace ClientApp.Services.IService {
    public interface IBusquedaRepository
    {
        Task<List<DataHomologacionEsquema>> PsBuscarPalabraAsync(string value, int pageNumber, int pageSize);
        Task<List<HomologacionEsquema>> FnHomologacionEsquemaTodoAsync();
        Task<HomologacionEsquema> FnHomologacionEsquemaAsync(int idHomologacionEsquema);
        Task<List<DataHomologacionEsquema>> FnHomologacionEsquemaDatoAsync(int idHomologacionEsquema, int idDataLakeOrganizacion);
    }
}