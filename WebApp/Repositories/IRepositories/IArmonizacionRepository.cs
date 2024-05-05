
using WebApp.Models;
using WebApp.Models.Dtos;

namespace WebApp.Repositories.IRepositories {
    public interface IArmonizacionRepository
    {
        ICollection<Armonizacion> ObtenerEtiquetas();
    }
}