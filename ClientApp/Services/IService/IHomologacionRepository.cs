using ClientApp.Models;

namespace ClientApp.Services.IService {
    public interface IHomologacionRepository
    {
        Task<List<VwHomologacion>> GetHomologacionsAsync(int valor);
        Task<VwHomologacion> GetHomologacionAsync(int idHomologacion);
        public Task<RespuestaRegistro> RegistrarOActualizar(VwHomologacion registro);
        Task<RespuestaRegistro> EliminarHomologacion(int idHomologacion);
    }
}