using AutoMapper;
using Microsoft.EntityFrameworkCore;
using prueba_tecnica_semi_senior_wolivo.Context;
using prueba_tecnica_semi_senior_wolivo.Interface;
using prueba_tecnica_semi_senior_wolivo.Mapper;
using prueba_tecnica_semi_senior_wolivo.Middlewares;
using prueba_tecnica_semi_senior_wolivo.Repository;
using prueba_tecnica_semi_senior_wolivo.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
// Add services to the container.

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProyectoService, ProyectoService>();
builder.Services.AddScoped<IActividadService, ActividadService>();
builder.Services.AddScoped<IReporteService, ReporteService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });


builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

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
