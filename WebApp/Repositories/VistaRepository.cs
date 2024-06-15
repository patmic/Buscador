using System.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Repositories.IRepositories;
using WebApp.Service;
using SharedApp.Models.Dtos;

namespace WebApp.Repositories
{
  public class VistaRepository(SqlServerDbContext dbContext) : IVistaRepository
  {
      private readonly SqlServerDbContext _bd = dbContext;
        public bool create(Vista data)
        {
            data.FechaCreacion = DateTime.Now;   
            data.FechaModifica = DateTime.Now;

            _bd.Vista.Add(data);
            return _bd.SaveChanges() >= 0 ? true : false;
        }
        public Vista find(int Id)
        {
          return _bd.Vista.AsNoTracking().FirstOrDefault(u => u.IdVista == Id);
        }
        public ICollection<Vista> findAll()
        {
            return _bd.Vista.AsNoTracking().Where(c => c.Estado.Equals("A")).ToList();
        }
        public ICollection<Vista> findBySystem(int idHomologacionSistema)
        {
            return _bd.Vista.AsNoTracking().Where(c => c.Estado.Equals("A") && c.IdHomologacionSistema == idHomologacionSistema).ToList();
        }
        public ICollection<PropiedadesTablaDto> GetProperties(string vistaNombre)
        {
            List<PropiedadesTablaDto> columnInfos = new List<PropiedadesTablaDto>();

            var connection = _bd.Database.GetDbConnection();
            connection.Open();

            using (var command = connection.CreateCommand())
            {
              command.CommandText = $@"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName";
              var tableNameParameter = command.CreateParameter();
              tableNameParameter.ParameterName = "@tableName";
              tableNameParameter.Value = vistaNombre;
              command.Parameters.Add(tableNameParameter);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                      columnInfos.Add(new PropiedadesTablaDto{
                        NombreColumna = reader["COLUMN_NAME"].ToString()
                      });
                    }
                }
            }

            connection.Close();
            return columnInfos;
        }
        public bool update(Vista newRecord)
        {
          var currentRecord = _bd.Vista.FirstOrDefault(u => u.IdVista == newRecord.IdVista);
          newRecord.FechaModifica = DateTime.Now;

          PropertyInfo[] propiedades = typeof(Vista).GetProperties();

          foreach (PropertyInfo propiedad in propiedades)
          {
            object valorModificado = propiedad.GetValue(newRecord);
            object valorExistente = propiedad.GetValue(currentRecord);

            if (valorModificado != null && !object.Equals(valorModificado, valorExistente))
            {
                propiedad.SetValue(currentRecord, valorModificado);
            }
          }

          _bd.Vista.Update(currentRecord);
          return _bd.SaveChanges() >= 0 ? true : false;
        }
  }
}