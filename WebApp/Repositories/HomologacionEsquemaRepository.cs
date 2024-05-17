using System.Data;
using System.Reflection;
using Dapper;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using WebApp.Service;

namespace WebApp.Repositories
{
  public class HomologacionEsquemaRepository(SqlServerDbContext dbContext) : IHomologacionEsquemaRepository
  {
    private readonly SqlServerDbContext _bd = dbContext;

    private readonly string? _connectionString;

    public HomologacionEsquema find(int Id)
    {
      return _bd.HomologacionEsquema.AsNoTracking().FirstOrDefault(u => u.IdHomologacionEsquema == Id);
    }

    public bool update(HomologacionEsquema newRecord)
    {
      var currentRecord = _bd.HomologacionEsquema.FirstOrDefault(u => u.IdHomologacionEsquema == newRecord.IdHomologacionEsquema);
      newRecord.FechaModifica = DateTime.Now;

      PropertyInfo[] propiedades = typeof(HomologacionEsquema).GetProperties();

      foreach (PropertyInfo propiedad in propiedades)
      {
        object valorModificado = propiedad.GetValue(newRecord);
        object valorExistente = propiedad.GetValue(currentRecord);

        if (valorModificado != null && !object.Equals(valorModificado, valorExistente))
        {
            propiedad.SetValue(currentRecord, valorModificado);
        }
      }

      _bd.HomologacionEsquema.Update(currentRecord);
      return _bd.SaveChanges() >= 0 ? true : false;
    }
  }
}