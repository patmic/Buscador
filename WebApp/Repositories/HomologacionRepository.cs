using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Repositories.IRepositories;
using WebApp.Service;

namespace WebApp.Repositories
{
  public class HomologacionRepository(SqlServerDbContext dbContext) : IHomologacionRepository
  {
    private readonly SqlServerDbContext _bd = dbContext;

    public bool create(Homologacion data)
    {
        data.FechaCreacion = DateTime.Now;   
        data.FechaModifica = DateTime.Now;

        _bd.Homologacion.Add(data);
        return _bd.SaveChanges() >= 0 ? true : false;
    }

    public Homologacion find(int Id)
    {
      return _bd.Homologacion.AsNoTracking().FirstOrDefault(u => u.IdHomologacion == Id);
    }

    public Homologacion? findByMostrarWeb(string filter)
    {
      return _bd.Homologacion.AsNoTracking().FirstOrDefault(u => u.MostrarWeb == filter);
    }

    public ICollection<Homologacion> findByParent(int valor)
    {
      return _bd.Homologacion.Where(c => c.IdHomologacionGrupo == valor && c.Estado.Equals("A")).OrderBy(c => c.MostrarWebOrden).ToList();
    }

    public bool update(Homologacion newRecord)
    {
      var currentRecord = _bd.Homologacion.FirstOrDefault(u => u.IdHomologacion == newRecord.IdHomologacion);
      newRecord.FechaModifica = DateTime.Now;

      PropertyInfo[] propiedades = typeof(Homologacion).GetProperties();

      foreach (PropertyInfo propiedad in propiedades)
      {
        object valorModificado = propiedad.GetValue(newRecord);
        object valorExistente = propiedad.GetValue(currentRecord);

        if (valorModificado != null && !object.Equals(valorModificado, valorExistente))
        {
            propiedad.SetValue(currentRecord, valorModificado);
        }
      }

      _bd.Homologacion.Update(currentRecord);
      return _bd.SaveChanges() >= 0 ? true : false;
    }
  }
}