using SharedApp.Models.Dtos;
using WebApp.Models;

namespace WebApp.Repositories.IRepositories {
  public interface IVistaRepository
  {

    bool update(Vista data);
    bool create(Vista data);
    Vista find(int Id);
    ICollection<Vista> findAll();
    ICollection<PropiedadesTablaDto> GetProperties(string vistaNombre);
    ICollection<Vista> findBySystem(int idHomologacionSistema);
  }
}