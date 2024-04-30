
using WebApp.Models;
using WebApp.Models.Dtos;

namespace WebApp.Repositories.IRepositories {
    public interface IBuscadorRepository
    {
        ICollection<Organizacion> BuscarPalabra(string field, string value);
        ICollection<Organizacion> BuscarPalabras();
    }
}