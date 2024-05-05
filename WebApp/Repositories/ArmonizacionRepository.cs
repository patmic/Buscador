using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Repositories.IRepositories;
using WebApp.Service;

namespace WebApp.Repositories
{
    public class ArmonizacionRepository(SqlServerDbContext dbContext) : IArmonizacionRepository
    {
      private readonly SqlServerDbContext _bd = dbContext;

      private readonly string? _connectionString;

      public ICollection<Armonizacion> ObtenerEtiquetas()
      {
        return _bd.Armonizacion.OrderBy(c => c.IdEtiqueta).ToList();
      }
  }
}