
using WebApp.Models;

namespace WebApp.Service.IService
{
    public interface IImportadorService
    {
        Boolean Importar(string[] path);
    }
}