// Utilizaremos el patrón Repository para esto.
using ClientApp.Models;

namespace ClientApp.Services.IService {
    public interface IBusquedaService
    {
        Task<ResultDataHomologacionEsquema> PsBuscarPalabraAsync(string paramJSON, int PageNumber, int RowsPerPage);
        Task<List<HomologacionEsquema>> FnHomologacionEsquemaTodoAsync();
        Task<HomologacionEsquema> FnHomologacionEsquemaAsync(int idHomologacionEsquema);
        Task<List<DataHomologacionEsquema>> FnHomologacionEsquemaDatoAsync(int idHomologacionEsquema, int idDataLakeOrganizacion);
    }
}