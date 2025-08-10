using DeveloperStore.CrossCutting;
using DeveloperStore.CrossCutting.DependencyInjection;
using DeveloperStore.Domain.Enums;
using DeveloperStore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Rebus.Config;
using Rebus.Transport.InMem;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Definir cultura global para pt-BR
var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Rebus
builder.Services.AddRebus(configure => configure
    .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "developerstore-queue")));

// Registro de Contexto do banco de dados
builder.Services.AddDbContext<SalesDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorCodesToAdd: null);
        });
});


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DisplayEnumConverter<SaleStatus>());
    });

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

//Cors Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigin", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseRouting();
app.UseCors("AllowAngularOrigin");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
