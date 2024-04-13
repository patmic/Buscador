
using Dapper;
using System.Data;
using WebApp.Models;
using MySql.Data.MySqlClient;

namespace WebApp.Repositories;
public class PeruRepository : IPeruRepository // Asumiendo que tienes una interfaz llamada IPeruRepository
{
    private readonly string _connectionString;
    public PeruRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("MySql");  
    }
    private IDbConnection CreateConnection()
    {
        return new MySqlConnection(_connectionString); // Usar MySqlConnection para MySQL
    }
    public async Task<IEnumerable<Organizacion>> GetOrganizacionAsync()
    {
        using IDbConnection conn = CreateConnection();
        string sql = "SELECT  id,nombre,direccion,telefono,email FROM ORGANIZACION"; 
        return await conn.QueryAsync<Organizacion>(sql);
    }

    public async Task<Organizacion> GetOrganizacionByIdAsync(int id)
    {
        using IDbConnection conn = CreateConnection();
        string sql = "SELECT  id,nombre,direccion,telefono,email FROM ORGANIZACION WHERE Id = @Id"; 
        Organizacion? organizacion = await conn.QueryFirstOrDefaultAsync<Organizacion>(sql, new { Id = id });
        return organizacion ?? throw new Exception("ORGANIZACION no econtrada en la base");
    }
}