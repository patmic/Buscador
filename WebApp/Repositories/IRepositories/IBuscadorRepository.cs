using WebApp.Models.Dtos;

namespace WebApp.Repositories.IRepositories {
    public interface IBuscadorRepository
    {
        ICollection<FnHomologacionEsquemaDataDto> PsBuscarPalabra(string value, int pageNumber, int pageSize);
        ICollection<EsquemaDto> FnHomologacionEsquemaTodo();
        HomologacionEsquemaDto FnHomologacionEsquema(int idHomologacionEsquema);
        ICollection<FnHomologacionEsquemaDataDto> FnHomologacionEsquemaDato(int idHomologacionEsquema, int idDataLakeOrganizacion);
    }
}