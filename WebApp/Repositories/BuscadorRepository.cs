using System.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using WebApp.Service;

namespace WebApp.Repositories
{
    public class BuscadorRepository: IBuscadorRepository
    {
        private readonly SqlServerDbContext _bd;
        public BuscadorRepository(SqlServerDbContext dbContext)
        {
            _bd = dbContext;
        }
        public ICollection<FnHomologacionEsquemaDataDto> PsBuscarPalabra(string value, int pageNumber, int pageSize)
        {
            var lstTem = _bd.Database.SqlQuery<FnHomologacionEsquemaData>($"exec psBuscarPalabra {value},{pageNumber},{pageSize}").AsNoTracking().ToList();
            return lstTem.Select(c => new FnHomologacionEsquemaDataDto()
            {
                IdDataLakeOrganizacion = c.IdDataLakeOrganizacion,
                DataEsquemaJson = JsonConvert.DeserializeObject<List<ColumnaEsquema>>(c.DataEsquemaJson)
            })
            .ToList();
        }
        public ICollection<EsquemaDto> FnHomologacionEsquemaTodo()
        {
            return _bd.Database.SqlQuery<EsquemaDto>($"select * from fnHomologacionEsquemaTodo()").AsNoTracking().OrderBy(c => c.MostrarWebOrden).ToList();
        }
        public HomologacionEsquemaDto FnHomologacionEsquema(int idHomologacionEsquema)
        {
            return _bd.Database.SqlQuery<HomologacionEsquemaDto>($"select * from fnHomologacionEsquema({idHomologacionEsquema})").AsNoTracking().FirstOrDefault();
        }
        public ICollection<FnHomologacionEsquemaDataDto> FnHomologacionEsquemaDato(int idHomologacionEsquema, int idDataLakeOrganizacion)
        {
            var lstTem = _bd.Database.SqlQuery<FnHomologacionEsquemaData>($"select * from fnHomologacionEsquemaDato({idHomologacionEsquema}, {idDataLakeOrganizacion})").AsNoTracking().ToList();
            return lstTem.Select(c => new FnHomologacionEsquemaDataDto()
            {
                IdDataLakeOrganizacion = c.IdDataLakeOrganizacion,
                IdHomologacionEsquema = c.IdHomologacionEsquema,
                DataEsquemaJson = JsonConvert.DeserializeObject<List<ColumnaEsquema>>(c.DataEsquemaJson)
            })
            .ToList();
        }
    }
}