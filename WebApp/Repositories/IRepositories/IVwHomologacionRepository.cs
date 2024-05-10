using WebApp.Models;

namespace WebApp.Repositories.IRepositories 
{
    public interface IVwHomologacionRepository
    {
        public ICollection<VwPais> ObtenerPais();
        ICollection<VwDimension> ObtenerDimension();
        ICollection<VwEsqAcredita> ObtenerEsqAcredita();
        ICollection<VwEstado> ObtenerEstado();
        ICollection<VwOrgAcredita> ObtenerOrgAcredita();
        ICollection<VwAlcanceRazonSocial> ObtenerAlcanceRazonSocial();
        ICollection<VwGrilla> ObtenerEtiquetaGrilla();
        ICollection<VwFiltro> ObtenerEtiquetaFiltros();
    }
}