using System;
using System.Data.SqlClient;

class Test_CAN_DB
{
    static void Main()
    {
        // Cadena de conexión a la base de datos
        string connectionString = "Server=PAT-PC;Database=CAN_DB;User Id=pat_mic;Password=michael;";

        // Establecer la conexión
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Consulta SQL para seleccionar datos de la tabla Homologacion
                string query = "SELECT * FROM Homologacion";

                // Ejecutar la consulta
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Obtener los resultados
                        while (reader.Read())
                        {
                            // Imprimir los resultados
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write(reader[i] + " ");
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine("Error al conectar a la base de datos: " + e.Message);
        }
    }
}
