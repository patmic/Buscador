using WebApp.Models;

namespace WebApp.Repositories.IRepositories 
{
    public interface IVwHomologacionRepository
    {
        public ICollection<VwPais> ObtenerPais();
        ICollection<VwTipoAcreditacion> ObtenerTipoAcreditacion();
        ICollection<VwDimension> ObtenerDimension();
        ICollection<VwEsqAcredita> ObtenerEsqAcredita();
        ICollection<VwEstado> ObtenerEstado();
        ICollection<VwOrgAcredita> ObtenerOrgAcredita();
    }
}