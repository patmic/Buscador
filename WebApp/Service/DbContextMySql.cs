using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Service
{
    public class DbContextMySql : DbContext
    {
        private readonly string _connectionString;

        public DbContextMySql(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }

        //Agregar los modelos
        public DbSet<Organizacion> Organizacion { get; set; }
    }
}
