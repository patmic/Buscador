using ClientApp.Models;

namespace ClientApp.Services.IService {
    public interface IVwHomologacionRepository
    {
        Task<List<VwHomologacion>> GetHomologacionAsync(string endpoint);
    }
}