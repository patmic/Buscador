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

        public ICollection<Organizacion> BuscarPalabra(string field, string value)
        {
            return _bd.Database.SqlQuery<Organizacion>($"SELECT * FROM fnBuscarPalabraUnica('{field}', '{value}')").ToList();
        }

        public ICollection<Organizacion> BuscarPalabras()
        {
            throw new NotImplementedException();
        }
    }
}