using WebApp.Models;

namespace WebApp.Repositories.IRepositories {
  public interface IDataLakeRepository
  {

    bool update(DataLake data);
    DataLake create(DataLake data);
    DataLake find(int Id);
    DataLake findBy(DataLake dataLake);
    ICollection<DataLake> findAll();
  }
}