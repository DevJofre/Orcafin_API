using Microsoft.EntityFrameworkCore;
using Orcafin.Application.Interfaces;
using Orcafin.Application.Services;
using Orcafin.Domain.Interfaces;
using Orcafin.Infrastructure.Context;
using Orcafin.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configuração do DbContext
builder.Services.AddDbContext<OrcafinDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de Dependência
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


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
