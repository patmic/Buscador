using System.Data;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using WebApp.Service;

namespace WebApp.Repositories
{
  public class BuscadorRepository: IBuscadorRepository
  {
    private readonly SqlServerDbContext _bd;
    public BuscadorRepository(SqlServerDbContext dbContext)
    {
        _bd = dbContext;
    }
    public ICollection<BuscadorOrganizacion> BuscarPalabra(string value)
    {
      Console.WriteLine(value);
        return _bd.Set<BuscadorOrganizacion>().FromSqlRaw("exec psBuscarPalabra {0}", value).ToList();
    }
    public DataLakeOrganizacion BuscarOrganizacion(int Id)
    {
      return _bd.DataLakeOrganizacion.AsNoTracking().FirstOrDefault(u => u.IdDataLakeOrganizacion == Id);
    }
    public ICollection<HomologacionEsquemaDto> ObtenerEsquemasRelacionados(int Id)
    {
      return _bd.Database.SqlQuery<HomologacionEsquemaDto>($"select * from fnHomologacionEsquema({Id})").OrderBy(c => c.MostrarWebOrden).ToList();
    }
    public ICollection<DataLakeOrganizacion> ObtenerOrganizacionesRelacionadas(int Id, int IdDataLake)
    {
      return _bd.DataLakeOrganizacion
        .Where(d => d.IdDataLakeOrganizacion != Id && d.IdDataLake == IdDataLake)
        .ToList();
    }
  }
}