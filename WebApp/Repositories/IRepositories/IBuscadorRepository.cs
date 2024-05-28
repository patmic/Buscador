
using WebApp.Models;
using WebApp.Models.Dtos;

namespace WebApp.Repositories.IRepositories {
    public interface IBuscadorRepository
    {
        ICollection<BuscadorOrganizacion> BuscarPalabra(string value);
        DataLakeOrganizacion BuscarOrganizacion(int Id);
        ICollection<DataLakeOrganizacion> ObtenerOrganizacionesRelacionadas(int Id, int IdDataLake);

        ICollection<HomologacionEsquema> ObtenerEsquemasRelacionados(int Id);
    }
}