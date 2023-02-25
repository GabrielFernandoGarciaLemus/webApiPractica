using Microsoft.EntityFrameworkCore;
using webApiPractica.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Inyeccion por dependencia  del String  de conexion a contexto
builder.Services.AddDbContext<equiposContext>(options =>
        options.UseSqlServer(
                builder.Configuration.GetConnectionString("equiposDbConnection")
         )
  );

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
