using WebApp.Models;

namespace WebApp.Repositories.IRepositories 
{
    public interface IVwHomologacionRepository
    {
        ICollection<VwGrilla> ObtenerEtiquetaGrilla();
        ICollection<VwFiltro> ObtenerEtiquetaFiltros();
        ICollection<IVwHomologacion> ObtenerFiltroDetalles(int IdHomologacion);
    }
}