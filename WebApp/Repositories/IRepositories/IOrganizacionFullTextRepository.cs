using WebApp.Models;

namespace WebApp.Repositories.IRepositories {
  public interface IOrganizacionFullTextRepository
  {

    bool update(OrganizacionFullText data);
    OrganizacionFullText create(OrganizacionFullText data);
    OrganizacionFullText find(int Id);
    OrganizacionFullText findBy(OrganizacionFullText dataLake);
    ICollection<OrganizacionFullText> findAll();
  }
}