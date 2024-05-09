using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Repositories.IRepositories;
using WebApp.Service;

namespace WebApp.Repositories
{
  public class BuscadorRepository: IBuscadorRepository
  {
    private readonly SqlServerDbContext _bd;

    private readonly string? _connectionString;
    public BuscadorRepository(SqlServerDbContext dbContext)
    {
        _bd = dbContext;
    }

    public ICollection<BuscadorOrganizacion> BuscarPalabra(string value, int field1, int field2, int field3, int field4)
{
    return _bd.Set<BuscadorOrganizacion>().FromSqlRaw(
      "exec psBuscarPalabra {0}, {1}, {2}, {3}, {4}", 
      value, field1, field2, field3, field4
    ).ToList();
}

    public ICollection<BuscadorOrganizacion> BuscarPalabras(string field1, string field2, string field3, string field4, string value)
    {
      throw new NotImplementedException();
    }
  }
}