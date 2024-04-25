using DataAccess.Service.IService;

namespace DataAccess.Service
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IConfiguration _configuration;

        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString(string key)
        {
            return _configuration.GetConnectionString(key);
        }

        public string GetValueString(string key)
        {
            return _configuration.GetValue<string>(key);
        }
    }
}