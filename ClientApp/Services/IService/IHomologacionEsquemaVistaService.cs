
using ClientApp.Models;
using SharedApp.Models.Dtos;

namespace ClientApp.Services.IService {
    public interface IHomologacionEsquemaVistaService
    {
        Task<RespuestaRegistro> Registrar(List<HomologacionEsquemaVistaDto> data);
        Task<List<HomologacionEsquemaVistaDto>> GetFindByVistaEsquemaAsync(int idVista, int idHomologacionEsquema);
    }
}