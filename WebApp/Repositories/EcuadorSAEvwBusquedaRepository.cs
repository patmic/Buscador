
using Dapper;
using Microsoft.Data.SqlClient;
using WebApp.Models;

namespace WebApp.Repositories;
public class EcuadorSAEvwBusquedaRepository : IEcuadorSAEvwBusquedaRepository
{
    private string sqlvwBusqueda = 
    "  SELECT AreaId "
    +" ,Acreditacion " 
    +" ,AcreditacionActividad " 
    +" ,Ubicacion " 
    +" ,UbicacionTipo " 
    +" ,Estado " 
    +" ,CodigoAcreditacion " 
    +" ,RazonSocial "
    +" FROM vwBusqueda ";
    private readonly IConfiguration _config;
    private SqlConnection GetConnection => new SqlConnection(_config.GetConnectionString("Mssql"));
    public EcuadorSAEvwBusquedaRepository(IConfiguration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }
    public async Task<IEnumerable<EcuadorSAEvwBusqueda>> GetALlAsync_SAEvwBusqueda()
    {
        using SqlConnection conn = GetConnection;
        return await conn.QueryAsync<EcuadorSAEvwBusqueda>(sqlvwBusqueda);
    }
    public async Task<EcuadorSAEvwBusqueda> GetByIdAsync_SAEvwBusqueda(int id)
    {
        using SqlConnection conn = GetConnection;
        string sql = sqlvwBusqueda + " WHERE AreaId = @Id";
        EcuadorSAEvwBusqueda? row = await conn.QueryFirstOrDefaultAsync<EcuadorSAEvwBusqueda>(sql, new { Id = id });
        return row ?? throw new Exception("EcuadorSAEvwBusqueda, no existe");
    }
}