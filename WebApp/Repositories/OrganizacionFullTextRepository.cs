using System.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Repositories.IRepositories;
using WebApp.Service;

namespace WebApp.Repositories
{
  public class OrganizacionFullTextRepository(SqlServerDbContext dbContext) : IOrganizacionFullTextRepository
  {
    private readonly SqlServerDbContext _bd = dbContext;

    public OrganizacionFullText create(OrganizacionFullText data)
    {
      data.IdOrganizacionFullText = 0;
      _bd.OrganizacionFullText.Add(data);
      _bd.SaveChanges();
      return data;
    }

    public OrganizacionFullText find(int Id)
    {
      return _bd.OrganizacionFullText.AsNoTracking().FirstOrDefault(u => u.IdOrganizacionFullText == Id);
    }

    public ICollection<OrganizacionFullText> findAll()
    {
        throw new NotImplementedException();
    }

    public OrganizacionFullText findBy(OrganizacionFullText dataLake)
    {
      throw new NotImplementedException();
    }

    public bool update(OrganizacionFullText newRecord)
    {
      throw new NotImplementedException();
    }
  }
}