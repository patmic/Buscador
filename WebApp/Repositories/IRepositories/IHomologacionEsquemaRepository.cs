using WebApp.Models;
using WebApp.Models.Dtos;

namespace WebApp.Repositories.IRepositories {
  public interface IHomologacionEsquemaRepository
  {

    bool update(HomologacionEsquema data);
    HomologacionEsquema find(int Id);
  }
}