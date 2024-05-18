using WebApp.WorkerService;
using WebApp.Repositories;
using WebApp.Repositories.IRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApp.Mappers;
using Microsoft.OpenApi.Models;
using WebApp.Service;
using Microsoft.EntityFrameworkCore;
using WebApp.Service.IService;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContextPool<MySqlDbContext>(options => {
    options.UseMySQL(builder.Configuration.GetConnectionString("Mysql"));
});

builder.Services.AddDbContextPool<SqlServerDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("Mssql-CanDb"));
});

builder.Services.AddControllers();

// WorkerService
builder.Services.AddHostedService<BackgroundWorkerService>();

// Api
builder.Services.AddScoped<IEcuadorSAEvwBusquedaRepository, EcuadorSAEvwBusquedaRepository>();
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IEcuadorRepository, EcuadorRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEndpointRepository, EndpointRepository>();
builder.Services.AddScoped<IUsuarioEndpointPermisoRepository, UsuarioEndpointPermisoRepository>();
builder.Services.AddScoped<IVwHomologacionRepository, VwHomologacionRepository>();
builder.Services.AddScoped<IBuscadorRepository, BuscadorRepository>();
builder.Services.AddScoped<IHomologacionEsquemaRepository, HomologacionEsquemaRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Agregar Automapper
builder.Services.AddAutoMapper(typeof(Mapper));

// Configuración de autenticación JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("ApiSettings:Secreta"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
        "Autenticación JWT usando el esquema Bearer. \r\n\r\n " +
        "Ingresa la palabra 'Bearer' seguida de un [espacio] y después tu token en el campo de abajo \r\n\r\n" +
        "Ejemplo: \"Bearer tkdknkdllskd\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
