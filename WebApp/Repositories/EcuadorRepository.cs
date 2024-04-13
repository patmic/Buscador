using Dapper;
using WebApp.Models;
using Microsoft.Data.SqlClient;

namespace WebApp.Repositories;
public class EcuadorRepository : IEcuadorRepository
{
    private readonly IConfiguration _config;
    private SqlConnection GetConnection => new SqlConnection(_config.GetConnectionString("Mssql"));
    public EcuadorRepository(IConfiguration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }
    public async Task<IEnumerable<Empresa>> GetEmpresasAsync()
    {
        using SqlConnection conn = GetConnection;
        string sql = "SELECT  EmpresaID ,Nombre ,Direccion  ,Telefono   FROM Empresa";
        return await conn.QueryAsync<Empresa>(sql);
    }
    public async Task<Empresa> GetEmpresasByIdAsync(int id)
    {
        using SqlConnection conn = GetConnection;
        string sql = "SELECT  EmpresaID ,Nombre ,Direccion  ,Telefono   FROM Empresa WHERE EmpresaID = @Id";
        Empresa? empresa = await conn.QueryFirstOrDefaultAsync<Empresa>(sql, new { Id = id });
        return empresa ?? throw new Exception("Empresa no econtrada en la base");
    }
 

    // public async Task AgregarAsync(Organizacion organizacion)
    // {
    //     using (var connection = Connection)
    //     {
    //         await connection.OpenAsync();
    //         await connection.ExecuteAsync("INSERT INTO Organizacion (Nombre) VALUES (@Nombre)", organizacion);
    //     }
    // }

    // public async Task ActualizarAsync(Organizacion organizacion)
    // {
    //     using (var connection = Connection)
    //     {
    //         await connection.OpenAsync();
    //         await connection.ExecuteAsync("UPDATE Organizacion SET Nombre = @Nombre WHERE Id = @Id", organizacion);
    //     }
    // }

    // public async Task EliminarAsync(int id)
    // {
    //     using (var connection = Connection)
    //     {
    //         await connection.OpenAsync();
    //         await connection.ExecuteAsync("DELETE FROM Organizacion WHERE Id = @Id", new { Id = id });
    //     }
    // }
}