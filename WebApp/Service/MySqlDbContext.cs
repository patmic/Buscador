using Microsoft.EntityFrameworkCore;

namespace WebApp.Service
{
    public class MySqlDbContext : DbContext
    {
        public MySqlDbContext()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.LazyLoadingEnabled = false;
        }
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.LazyLoadingEnabled = false;
        }
    }
}