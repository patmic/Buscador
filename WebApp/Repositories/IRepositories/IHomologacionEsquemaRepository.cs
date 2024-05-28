using WebApp.Models;

namespace WebApp.Repositories.IRepositories {
  public interface IHomologacionEsquemaRepository
  {

    bool update(HomologacionEsquema data);
    bool create(HomologacionEsquema data);
    HomologacionEsquema find(int Id);
    ICollection<HomologacionEsquema> findAll();
  }
}