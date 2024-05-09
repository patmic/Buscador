
using WebApp.Models;
using WebApp.Models.Dtos;

namespace WebApp.Repositories.IRepositories {
    public interface IBuscadorRepository
    {
        ICollection<BuscadorOrganizacion> BuscarPalabra(string value, int field1, int field2, int field3, int field4);
        ICollection<BuscadorOrganizacion> BuscarPalabras(string field1, string field2, string field3, string field4, string value);
    }
}