using System.Data;
using Microsoft.Data.SqlClient;
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
        public object PsBuscarPalabra(string paramJSON, int PageNumber, int RowsPerPage)
        {
            var rowsTotal = new SqlParameter
            {
                ParameterName = "@RowsTotal",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var lstTem = _bd.Database.SqlQueryRaw<FnHomologacionEsquemaData>(
                "exec psBuscarPalabra @paramJSON, @PageNumber, @RowsPerPage, @RowsTotal OUT",
                new SqlParameter("@paramJSON", paramJSON),
                new SqlParameter("@PageNumber", PageNumber),
                new SqlParameter("@RowsPerPage", RowsPerPage),
                rowsTotal
            ).AsNoTracking().ToList();

            return new {
                Data = lstTem.Select(c => new FnHomologacionEsquemaDataDto()
                {
                    IdDataLakeOrganizacion = c.IdDataLakeOrganizacion,
                    DataEsquemaJson = JsonConvert.DeserializeObject<List<ColumnaEsquema>>(c.DataEsquemaJson)
                })
                .ToList(),
                TotalCount = (int) rowsTotal.Value
            };
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