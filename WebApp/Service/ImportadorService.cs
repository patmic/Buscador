
using ExcelDataReader;
using WebApp.Service.IService;
using WebApp.Models;
using System.Data;
using WebApp.Repositories.IRepositories;
using WebApp.Repositories;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace WebApp.Service.IService
{
  public class ImportadorService(IDataLakeRepository dataLakeRepository, IDataLakeOrganizacionRepository dataLakeOrganizacionRepository, IOrganizacionFullTextRepository organizacionFullTextRepository) : IImportadorService
    {
      private IDataLakeRepository _repositoryDL = dataLakeRepository;
      private IDataLakeOrganizacionRepository _repositoryDLO = dataLakeOrganizacionRepository;
      private IOrganizacionFullTextRepository _repositoryOFT = organizacionFullTextRepository;
      private string connectionString = "Server=localhost,1434;Initial Catalog=CAN_DB;User ID=sa;Password=pat_mic_DBKEY;TrustServerCertificate=True";

      public Boolean Importar(string path) 
      {
        string[] views =  ["GRILLA", "ESQ_01", "ESQ_02"];
        foreach (string view in views)
        {
          // if (!Leer(view)) { return false; }
        }
          return Leer("vwEsq01");
      }

      public Boolean Leer(string viewName)
      {
        
        string query = "SELECT * FROM " + viewName;
        string updateQuery = $"UPDATE DataLakeOrganizacion SET Estado = 'X' where IdDataLakeOrganizacion <= {_repositoryDLO.getLastId()}";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
          SqlCommand command = new SqlCommand(query, connection);
          SqlDataAdapter adapter = new SqlDataAdapter(command);
          DataSet dataSet = new DataSet();

          try
          {
            connection.Open();
            adapter.Fill(dataSet);
            DataLake? dataLake = null;
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
              Console.WriteLine(updateQuery);
              SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
              updateCommand.ExecuteNonQuery();
            } else {
              Console.WriteLine("No tables found");
              return false;
            }
            DataColumnCollection columns = dataSet.Tables[0].Columns;
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
              dataLake = getDatalake(row, dataLake);
              if (dataLake == null) { return false; }
              
              DataLakeOrganizacion dataLakeOrganizacion = addDataLakeOrganizacion(row, dataLake, columns);
              if (dataLakeOrganizacion == null) { return false; }

              addOrganizacionFullText(row, columns, dataLakeOrganizacion.IdDataLakeOrganizacion);
            }
          }
          catch (Exception ex)
          {
            Console.WriteLine(ex.Message);
            return false;
          }
          finally
          {
            connection.Close();
          }

          return true;
        }
      }

      DataLake? getDatalake(DataRow row, DataLake? dataLake)
      {
        if (dataLake == null) {
          return buildDatalake(row);
        } else
        {
          if (row[0].ToString().Equals(dataLake?.DataTipo?.ToString()) &&
              row[1].ToString().Equals(dataLake?.DataSistemaOrigen?.ToString()) &&
              row[2].ToString().Equals(dataLake?.DataSistemaOrigenId?.ToString()))
          {
            if (DateTime.Parse(row[3]?.ToString() ?? "01/01/1900") > dataLake.DataSistemaFecha)
            {
              dataLake.DataSistemaFecha = DateTime.Parse(row[3]?.ToString() ?? "01/01/1900");
              dataLake.Estado = "A";
              dataLake.DataFechaCarga = DateTime.Now;
              return _repositoryDL.update(dataLake);
            }
            else if (DateTime.Parse(row[3]?.ToString() ?? "01/01/1900") == dataLake.DataSistemaFecha)
            {
              return null;
            }
            return dataLake;
          } else 
          {
            return buildDatalake(row);
          }
        }
      }

      DataLake buildDatalake(DataRow row)
      {
        DataLake tmpDataLake = new DataLake
        {
          DataTipo = row[0].ToString(),
          DataSistemaOrigen = row[1].ToString(),
          DataSistemaOrigenId = row[2].ToString()
        };

        var existingDataLake = _repositoryDL.findBy(tmpDataLake);
        if (existingDataLake != null)
        {
          existingDataLake.DataSistemaFecha = DateTime.Parse(row[3]?.ToString() ?? "01/01/1900");
          _repositoryDL.update(existingDataLake);
          return existingDataLake;
        }
        else
        {
          tmpDataLake.Estado = "A";
          tmpDataLake.DataSistemaFecha = DateTime.Parse(row[3]?.ToString() ?? "01/01/1900");
          tmpDataLake.DataFechaCarga = DateTime.Now;
          return _repositoryDL.create(tmpDataLake);
        }
      }

      DataLakeOrganizacion addDataLakeOrganizacion(DataRow row, DataLake dataLake, DataColumnCollection columns)
      {
        return _repositoryDLO.create(new DataLakeOrganizacion
          {
            IdDataLakeOrganizacion = 0,
            IdDataLake = dataLake.IdDataLake,
            IdHomologacionEsquema = int.Parse(row[4].ToString()),
            DataEsquemaJson = buildDataLakeJson(row, columns),
            Estado = "A"
          });
      }
     
      bool addOrganizacionFullText(DataRow row, DataColumnCollection columns, int dataLakeOrganizacionId)
      {
        int columnsCount = columns.Count;
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
            IdHomologacion = int.Parse(columns[col].ColumnName.Substring(1)),
            FullTextOrganizacion = row[col].ToString()
          }) != null ? result : false;
        }
        return result;
      }
      
      string buildDataLakeJson(DataRow row, DataColumnCollection columns)
      {
        if (columns.Count < 5)
        {
          return "[]";
        }
        string json = "[";
        for (int col = 5; col < columns.Count; col++)
        {
          json += "{ \"IdHomologacion\": \"" + columns[col].ColumnName.Substring(1) + "\", \"Data\": \"" + columns[col].ColumnName.Substring(1) + " " + row[col].ToString() + "\" },";
        }
        return json.TrimEnd(',') + "]";
      }
  }
}