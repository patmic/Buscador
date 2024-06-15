using SharedApp.Models.Dtos;

namespace ClientApp.Services.IService {
    public interface IVistaService
    {
        Task<List<VistaDto>> GetFindBySystemAsync(int idHomologacionSistema);
        Task<List<string>> GetPropertiesAsync(string vistaNombre);
    }
}