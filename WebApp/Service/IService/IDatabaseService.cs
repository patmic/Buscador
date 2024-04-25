namespace DataAccess.Service.IService
{
    public interface IDatabaseService
    {
        string GetConnectionString(string key);
        string GetValueString(string key);
    }
}