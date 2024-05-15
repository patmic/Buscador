
using System.Data;
using WebApp.Repositories.IRepositories;
using WebApp.Models.Dtos;
using WebApp.Service;

namespace WebApp.Repositories
{
    public class EndpointRepository : IEndpointRepository
    {
        private readonly SqlServerDbContext _bd;

        public EndpointRepository(SqlServerDbContext dbContext)
        {
            _bd = dbContext;
        }

        public Models.Endpoint GetEndpoint(int endpointId)
        {
            return _bd.Endpoint.FirstOrDefault(c => c.IdEndpoint == endpointId);
        }

        public ICollection<Models.Endpoint> GetEndpoints()
        {
            return _bd.Endpoint.OrderBy(c => c.IdEndpoint).ToList();
        }

        public async Task<Models.Endpoint> Registro(EndpointRegistroDto endpointRegistroDto)
        {
            Models.Endpoint endpoint = new Models.Endpoint()
            {
                Nombre = endpointRegistroDto.Nombre,
                Url = endpointRegistroDto.Url,
                FechaCrea = DateTime.Now,
                FechaModifica = DateTime.Now
            };

            _bd.Endpoint.Add(endpoint);
            await _bd.SaveChangesAsync();
            return endpoint;
        }

        public bool IsUniqueUser(string nombre, string url)
        {
            var endpointbd = _bd.Endpoint.FirstOrDefault(u => u.Nombre == nombre && u.Url == url);
            if (endpointbd == null)
            {
                return true;
            }

            return false;
        }
    }
}