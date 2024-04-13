using System.Data;
using System.Data.SQLite;
using Dapper;
using WebApp.Models;

namespace WebApp.Repositories;

public class PersonaRepository: IPersonaRepository
{
    private readonly IConfiguration _config;
    private readonly string? _connectionString;
    public PersonaRepository(IConfiguration config)
    {
        _config = config;
       // config = config ?? throw new ArgumentNullException(nameof(config));
        _connectionString = _config.GetConnectionString("Default");
    }
    private IDbConnection GetConnection()
    {
        using IDbConnection conn = new SQLiteConnection(_connectionString);
        return conn;
    }
    public async Task<IEnumerable<Persona>> GetPersonasAsync()
    {
        using IDbConnection conn = new SQLiteConnection(_connectionString);
        string sql = "select * from Persona";
        var personas = await conn.QueryAsync<Persona>(sql);
        return personas;
    }
    public async Task<Persona> GetPersonasByIdAsync(int id)
    {
        using IDbConnection conn = new SQLiteConnection(_connectionString);
        string sql = "select * from Persona where id = @id";
        var p = await conn.QueryFirstOrDefaultAsync<Persona>(sql, new { id });
        return p;
    }
    public async Task<Persona> AddPersonaAsync(Persona p)
    {
        using IDbConnection conn = new SQLiteConnection(_connectionString);
        string sql = @"insert into Persona (nombre, email) values (@nombre, @email);
                      select last_insert_rowid()";
        int createdId = await conn.ExecuteScalarAsync<int>(sql, new { p.nombre, p.email });
        p.id = createdId;
        return p;
    }
    public async Task UpdatePersonaAsync(Persona p)
    {
        using IDbConnection conn = new SQLiteConnection(_connectionString);
        string sql = @"update Persona set nombre=@nombre, email=@email where id =@id";
        await conn.ExecuteAsync(sql, new { nombre = p.nombre, email = p.email, id = p.id });
    }
    public async Task DeletePersonaAsync(int id)
    {
        using IDbConnection conn = new SQLiteConnection(_connectionString);
        string sql = @"delete from Persona where id =@id";
        int createdId = await conn.ExecuteAsync(sql, new { id });
    }
}
