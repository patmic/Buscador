using WebApp.WorkerService;
using WebApp.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// WorkerService
builder.Services.AddHostedService<BackgroundWorkerService>();
// Api
builder.Services.AddTransient<IEcuadorSAEvwBusquedaRepository, EcuadorSAEvwBusquedaRepository>();

builder.Services.AddTransient<IPersonaRepository, PersonaRepository>();
builder.Services.AddTransient<IEcuadorRepository, EcuadorRepository>();
builder.Services.AddTransient<IPeruRepository, PeruRepository>();

var app = builder.Build();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.MapControllers();
app.Run();
