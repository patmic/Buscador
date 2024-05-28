using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
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
    public ICollection<HomologacionEsquema> ObtenerEsquemasRelacionados(int Id)
    {
      var query = @"
        SELECT *
        FROM HomologacionEsquema
        WHERE IdHomologacionEsquema IN (
            SELECT IdHomologacionEsquema
            FROM DataLakeOrganizacion
            WHERE IdDataLakeOrganizacion != @Id AND IdDataLake IN (
                SELECT IdDataLake
                FROM DataLakeOrganizacion
                WHERE IdDataLakeOrganizacion = @Id
            )
        ) ORDER BY MostrarWebOrden";

      return _bd.HomologacionEsquema.FromSqlRaw(query, new SqlParameter("@Id", Id)).ToList();
    }

    public ICollection<DataLakeOrganizacion> ObtenerOrganizacionesRelacionadas(int Id, int IdDataLake)
    {
      return _bd.DataLakeOrganizacion
        .Where(d => d.IdDataLakeOrganizacion != Id && d.IdDataLake == IdDataLake)
        .ToList();
    }
  }
}