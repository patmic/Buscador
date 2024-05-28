using WebApp.Models;

namespace WebApp.Repositories.IRepositories {
  public interface IHomologacionRepository
  {

    bool update(Homologacion data);
    bool create(Homologacion data);
    Homologacion find(int Id);
    ICollection<Homologacion> findByParent(int valor);
  }
}