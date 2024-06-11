namespace WebApp.Service.IService
{
    public interface IPasswordService
    {
        string GenerateTemporaryPassword(int length);
    }
}