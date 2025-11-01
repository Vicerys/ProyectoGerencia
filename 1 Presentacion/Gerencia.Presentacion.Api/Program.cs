using AutorizacionJwtServicio;
using Gerencia.ReglasdeNegocios.Negocios;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddScoped<TipoPuestoNegocio>();
builder.Services.AddScoped<EmpleadoNegocio>();
builder.Services.AddScoped<ProyectoNegocio>();
builder.Services.AddScoped<EmpleadoProyectoNegocio>();
builder.Services.AddScoped<EstadoNegocio>();
builder.Services.AddScoped<ImportanciaNegocio>();
builder.Services.AddScoped<DisponibilidadNegocio>();
builder.Services.AddScoped<TareaNegocio>();
builder.Services.AddScoped<EquipoNegocio>();
builder.Services.AddScoped<ProcesoNegocio>();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddScoped<UnityofWork>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
