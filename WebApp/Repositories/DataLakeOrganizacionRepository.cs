using System.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Repositories.IRepositories;
using WebApp.Service;

namespace WebApp.Repositories
{
  public class DataLakeOrganizacionRepository(SqlServerDbContext dbContext) : IDataLakeOrganizacionRepository
  {
    private readonly SqlServerDbContext _bd = dbContext;

    public DataLakeOrganizacion create(DataLakeOrganizacion data)
    {
      data.IdDataLakeOrganizacion = 0;
      _bd.DataLakeOrganizacion.Add(data);
      _bd.SaveChanges();
      return data;
    }

    public DataLakeOrganizacion find(int Id)
    {
      return _bd.DataLakeOrganizacion.AsNoTracking().FirstOrDefault(u => u.IdDataLakeOrganizacion == Id);
    }

    public ICollection<DataLakeOrganizacion> findAll()
    {
      return _bd.DataLakeOrganizacion.AsNoTracking().Where(c => c.Estado.Equals("A", StringComparison.Ordinal)).OrderBy(c => c.IdDataLakeOrganizacion).ToList();
    }

    public DataLakeOrganizacion findBy(DataLakeOrganizacion dataLake)
    {
      throw new NotImplementedException();
    }

    public bool update(DataLakeOrganizacion newRecord)
    {
      var currentRecord = _bd.DataLakeOrganizacion.FirstOrDefault(u => u.IdDataLakeOrganizacion == newRecord.IdDataLake);

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

      _bd.DataLakeOrganizacion.Update(currentRecord);
      return _bd.SaveChanges() >= 0 ? true : false;
    }

    public int getLastId()
    {
      return _bd.DataLakeOrganizacion.AsNoTracking().Max(c => c.IdDataLakeOrganizacion);
    }

    public bool deleteOldRecords(int IdHomologacionEsquema)
    {
      var records = _bd.DataLakeOrganizacion.Where(c => c.IdHomologacionEsquema == IdHomologacionEsquema).ToList();
      var deletedRecordIds = new List<int?>();

      foreach (var record in records)
      {
        record.Estado = "X";
        deletedRecordIds.Add(record.IdDataLakeOrganizacion);
      }

      _bd.DataLakeOrganizacion.UpdateRange(records);
      _bd.SaveChanges();

      var deletedOrganizacionFullTextRecords = _bd.OrganizacionFullText.Where(o => deletedRecordIds.Contains(o.IdDataLakeOrganizacion)).ToList();
      Console.WriteLine($"Deleted OrganizacionFullText records: {deletedOrganizacionFullTextRecords.Count}");
      _bd.OrganizacionFullText.RemoveRange(deletedOrganizacionFullTextRecords);
      _bd.SaveChanges();

      return true;
    }

  }
}