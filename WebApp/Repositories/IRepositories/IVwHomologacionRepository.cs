using WebApp.Models;

namespace WebApp.Repositories.IRepositories 
{
    public interface IVwHomologacionRepository
    {
        public ICollection<VwPais> ObtenerPais();
        ICollection<VwRazonSocial> ObtenerRazonSocial();
        ICollection<VwAlcance> ObtenerAlcance();
        ICollection<VwEsqAcredita> ObtenerEsqAcredita();
        ICollection<VwEstado> ObtenerEstado();
        ICollection<VwOrgAcredita> ObtenerOrgAcredita();
    }
}