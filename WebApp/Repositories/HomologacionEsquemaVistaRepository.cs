using System.Data;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Repositories.IRepositories;
using WebApp.Service;

namespace WebApp.Repositories
{
  public class HomologacionEsquemaVistaRepository(SqlServerDbContext dbContext) : IHomologacionEsquemaVistaRepository
  {
      private readonly SqlServerDbContext _bd = dbContext;
        public bool create(List<HomologacionEsquemaVista> data)
        {
            var itemsDelete = _bd.HomologacionEsquemaVista.Where(
              c => c.Estado.Equals("A") &&
              c.IdHomologacionEsquema == data[0].IdHomologacionEsquema &&
              c.IdVista == data[0].IdVista
            );

            if (itemsDelete.Any())
            {
               _bd.HomologacionEsquemaVista.RemoveRange(itemsDelete);
            }

            foreach(var item in data) {
              item.FechaCreacion = DateTime.Now;
              item.FechaModifica = DateTime.Now;

              _bd.HomologacionEsquemaVista.Add(item);
            }

            return _bd.SaveChanges() >= 0 ? true : false;
        }
        public HomologacionEsquemaVista find(int Id)
        {
          return _bd.HomologacionEsquemaVista.AsNoTracking().FirstOrDefault(u => u.IdVista == Id);
        }
        public ICollection<HomologacionEsquemaVista> findAll()
        {
            return _bd.HomologacionEsquemaVista.AsNoTracking().Where(c => c.Estado.Equals("A")).ToList();
        }
        public ICollection<HomologacionEsquemaVista> findByEsquema(int idVista, int idHomologacionEsquema)
        {
            return _bd.HomologacionEsquemaVista.AsNoTracking().Where(
              c => c.Estado.Equals("A") &&
              c.IdHomologacionEsquema == idHomologacionEsquema &&
              c.IdVista == idVista
            ).ToList();
        }
  }
}