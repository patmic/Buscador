using WebApp.Models;

namespace WebApp.Repositories.IRepositories {
  public interface IDataLakeRepository
  {

    DataLake? update(DataLake data);
    DataLake create(DataLake data);
    DataLake find(int Id);
    DataLake? findBy(DataLake dataLake);
    ICollection<DataLake> findAll();
  }
}