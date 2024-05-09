using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Service
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.LazyLoadingEnabled = false;
        }
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.LazyLoadingEnabled = false;
        }
        // modulo de seguridad
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Models.Endpoint> Endpoint { get; set; }
        public DbSet<UsuarioEndpointPermiso> UsuarioEndpointPermiso { get; set; }
        // modulo de prueba a otra bd
        public DbSet<Empresa> Empresa { get; set; }
        // vistas
        public DbSet<VwPais> VwPais { get; set; }
        public DbSet<VwTipoAcreditacion> VwTipoAcreditacion { get; set; }
        public DbSet<VwOrgAcredita> VwOrgAcredita { get; set; }
        public DbSet<VwEstado> VwEstado { get; set; }
        public DbSet<VwEsqAcredita> VwEsqAcredita { get; set; }
        public DbSet<VwDimension> VwDimension { get; set; }
        public DbSet<DataLakeOrganizacion> DataLakeOrganizacion { get; set; }
        public DbSet<Armonizacion> Armonizacion { get; set; }
        public DbSet<BuscadorOrganizacion> BuscadorOrganizacion { get; set; }
    }
}