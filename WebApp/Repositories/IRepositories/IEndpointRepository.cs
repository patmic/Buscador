using WebApp.Models.Dtos;

namespace WebApp.Repositories.IRepositories {
    public interface IEndpointRepository
    {
        ICollection<Models.Endpoint> GetEndpoints();
        Models.Endpoint GetEndpoint(int endpointId);
        Task<Models.Endpoint> Registro(EndpointRegistroDto endpointRegistroDto);
        bool IsUniqueUser(string nombre, string url);
    }
}