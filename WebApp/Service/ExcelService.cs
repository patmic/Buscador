
using ExcelDataReader;
using WebApp.Service.IService;
using WebApp.Models;
using System.Data;
using WebApp.Repositories.IRepositories;
using WebApp.Repositories;

namespace WebApp.Service.IService
{
  public class ExcelService(IDataLakeRepository dataLakeRepository, IDataLakeOrganizacionRepository dataLakeOrganizacionRepository, IOrganizacionFullTextRepository organizacionFullTextRepository) : IExcelService
    {
      private IDataLakeRepository _repositoryDL = dataLakeRepository;
      private IDataLakeOrganizacionRepository _repositoryDLO = dataLakeOrganizacionRepository;
      private IOrganizacionFullTextRepository _repositoryOFT = organizacionFullTextRepository;
      
      public Boolean ImportarExcel(string path) 
      {
        Leer(path, "Esq1");
          return true;
      }

      public Boolean Leer(string fileSrc, string worksheet)
      {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        using (var stream = File.Open(fileSrc, FileMode.Open, FileAccess.Read))
        {
          using (var reader = ExcelReaderFactory.CreateReader(stream))
          {
            var configuration = new ExcelDataSetConfiguration
            {
              ConfigureDataTable = _ => new ExcelDataTableConfiguration
              {
                UseHeaderRow = true
              }
            };

            var DataSet = reader.AsDataSet(configuration);

            if (DataSet.Tables.Count > 0)
            {
              var dataTable = DataSet.Tables[worksheet];
              DataLake dataLake = null;
              for (int i = 0; i < dataTable.Rows.Count; i++)
              {
                dataLake = getDatalake(dataTable, i, dataLake);
                DataLakeOrganizacion dataLakeOrganizacion = addDataLakeOrganizacion(dataTable, i, dataLake);

                addOrganizacionFullText(dataTable, i, dataLakeOrganizacion.IdDataLakeOrganizacion);
              }
              return true;
            } else {
              Console.WriteLine("No tables found");
              return false;
            }
          }
        }
        return true;
      }

      DataLake getDatalake(DataTable dataTable, int row, DataLake dataLake)
      {
        if (dataLake == null) {
          return buildDatalake(dataTable, row);
        } else
        {
          if (dataTable.Rows[row][0].ToString().Equals(dataLake?.DataTipo?.ToString()) &&
              dataTable.Rows[row][1].ToString().Equals(dataLake?.DataSistemaOrigen?.ToString()) &&
              dataTable.Rows[row][2].ToString().Equals(dataLake?.DataSistemaOrigenId?.ToString()))
          {
            if (DateTime.Parse(dataTable.Rows[row][3]?.ToString() ?? "01/01/1900") > dataLake.DataSistemaFecha)
            {
              dataLake.DataSistemaFecha = DateTime.Parse(dataTable.Rows[row][3]?.ToString() ?? "01/01/1900");
              dataLake.Estado = "A";
              dataLake.DataFechaCarga = DateTime.Now;
              return _repositoryDL.update(dataLake);
            }
            return dataLake;
          } else 
          {
            return buildDatalake(dataTable, row);
          }
        }
      }

      DataLake buildDatalake(DataTable dataTable, int row)
      {
        DataLake tmpDataLake = new DataLake
        {
          DataTipo = dataTable.Rows[row][0].ToString(),
          DataSistemaOrigen = dataTable.Rows[row][1].ToString(),
          DataSistemaOrigenId = dataTable.Rows[row][2].ToString(),
          DataSistemaFecha = DateTime.Parse(dataTable.Rows[row][3]?.ToString() ?? "01/01/1900")
        };

        var existingDataLake = _repositoryDL.findBy(tmpDataLake);
        if (existingDataLake != null)
        {
          return existingDataLake;
        }
        else
        {
          tmpDataLake.Estado = "A";
          tmpDataLake.DataFechaCarga = DateTime.Now;
          return _repositoryDL.create(tmpDataLake);
        }
      }

      DataLakeOrganizacion addDataLakeOrganizacion(DataTable dataTable, int row, DataLake dataLake)
      {
        return _repositoryDLO.create(new DataLakeOrganizacion
          {
            IdDataLakeOrganizacion = 0,
            IdDataLake = dataLake.IdDataLake,
            IdHomologacionEsquema = int.Parse(dataTable.Rows[row][4].ToString()),
            DataEsquemaJson = buildDataLakeJson(dataTable, row),
            Estado = "A"
          });
      }
      bool addOrganizacionFullText(DataTable dataTable, int row, int dataLakeOrganizacionId)
      {
        int columnsCount = dataTable.Columns.Count;
        if (columnsCount < 5)
        {
          return false;
        }
        Boolean result = true;
        for (int col = 5; col < columnsCount; col++)
        {
          result = _repositoryOFT.create(new OrganizacionFullText
          {
            IdOrganizacionFullText = 0,
            IdDataLakeOrganizacion = dataLakeOrganizacionId,
            IdHomologacion = int.Parse(dataTable.Columns[col].ColumnName),
            FullTextOrganizacion = dataTable.Rows[row][col].ToString()
          }) != null ? result : false;
        }
        return result;
      }
      string buildDataLakeJson(DataTable dataTable, int row)
      {
        int columnsCount = dataTable.Columns.Count;
        if (columnsCount < 5)
        {
          return "[]";
        }
        string json = "[";
        for (int col = 5; col < columnsCount; col++)
        {
          json += "{ \"IdHomologacion\": \"" + dataTable.Columns[col].ColumnName + "\", \"Data\": \"" + dataTable.Columns[col].ColumnName + " " + dataTable.Rows[row][col].ToString() + "\" },";
        }
        return json.TrimEnd(',') + "]";
      }
  }
}