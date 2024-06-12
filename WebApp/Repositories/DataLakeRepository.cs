using System.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Repositories.IRepositories;
using WebApp.Service;

namespace WebApp.Repositories
{
  public class DataLakeRepository(SqlServerDbContext dbContext) : IDataLakeRepository
  {
    private readonly SqlServerDbContext _bd = dbContext;

    public DataLake create(DataLake data)
    {
      data.IdDataLake = 0;
      _bd.DataLake.Add(data);
      _bd.SaveChanges();
      return data;
    }

    public DataLake find(int Id)
    {
      return _bd.DataLake.AsNoTracking().FirstOrDefault(u => u.IdDataLake == Id);
    }

    public ICollection<DataLake> findAll()
    {
        return _bd.DataLake.AsNoTracking().Where(c => c.Estado.Equals("A")).OrderBy(c => c.DataFechaCarga).ToList();
    }

    public DataLake? findBy(DataLake dataLake)
    {
      return _bd.DataLake.AsNoTracking().FirstOrDefault(
        u => u.DataTipo == dataLake.DataTipo &&
             u.DataSistemaOrigen == dataLake.DataSistemaOrigen &&
             u.DataSistemaOrigenId == dataLake.DataSistemaOrigenId
            );
    }

    public DataLake? update(DataLake newRecord)
    {
      var currentRecord = _bd.DataLake.FirstOrDefault(u => u.IdDataLake == newRecord.IdDataLake);
      newRecord.DataSistemaFecha = DateTime.Now;

      PropertyInfo[] propiedades = typeof(DataLake).GetProperties();

      foreach (PropertyInfo propiedad in propiedades)
      {
        object valorModificado = propiedad.GetValue(newRecord);
        object valorExistente = propiedad.GetValue(currentRecord);

        if (valorModificado != null && !object.Equals(valorModificado, valorExistente))
        {
            propiedad.SetValue(currentRecord, valorModificado);
        }
      }

      _bd.DataLake.Update(currentRecord);
      return _bd.SaveChanges() >= 0 ? currentRecord : null;
    }
  }
}