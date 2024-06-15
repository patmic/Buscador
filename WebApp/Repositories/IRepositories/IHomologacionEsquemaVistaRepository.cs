using SharedApp.Models.Dtos;
using WebApp.Models;

namespace WebApp.Repositories.IRepositories {
  public interface IHomologacionEsquemaVistaRepository
  {
    bool create(List<HomologacionEsquemaVista> data);
    HomologacionEsquemaVista find(int Id);
    ICollection<HomologacionEsquemaVista> findAll();
    ICollection<HomologacionEsquemaVista> findByEsquema(int idVista, int idHomologacionEsquema);
  }
}