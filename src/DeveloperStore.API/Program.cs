using DeveloperStore.CrossCutting;
using DeveloperStore.CrossCutting.DependencyInjection;
using DeveloperStore.Domain.Enums;
using DeveloperStore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Rebus.Config;
using Rebus.Transport.InMem;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Rebus
builder.Services.AddRebus(configure => configure
    .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "developerstore-queue")));

// Registro de Contexto do banco de dados
builder.Services.AddDbContext<SalesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DisplayEnumConverter<SaleStatus>());
    });

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

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
