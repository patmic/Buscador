using WebApp.Models;

namespace WebApp.Repositories.IRepositories {
  public interface IDataLakeOrganizacionRepository
  {

    bool update(DataLakeOrganizacion data);
    DataLakeOrganizacion create(DataLakeOrganizacion data);
    DataLakeOrganizacion find(int Id);
    DataLakeOrganizacion findBy(DataLakeOrganizacion dataLakeOrganizacion);
    ICollection<DataLakeOrganizacion> findAll();
    int getLastId();
  }
}