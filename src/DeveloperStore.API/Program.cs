using AutoMapper;
using DeveloperStore.CrossCutting.Mappers;
using DeveloperStore.Domain.Interfaces;
using DeveloperStore.Infrastructure.Context;
using DeveloperStore.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rebus.Config;
using Rebus.Transport.InMem;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ServiceFactory>(sp => type => sp.GetRequiredService(type));
builder.Services.AddSingleton<IMediator, Mediator>();

// Register MediatR handlers

//Automaper
builder.Services.AddSingleton<IMapper>(sp =>
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<MappingProfile>();
    });

    return config.CreateMapper();
});


//Rebus
builder.Services.AddRebus(configure => configure
    .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "developerstore-queue")));

// Registro de Repositories

builder.Services.AddDbContext<SalesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISaleRepository, SaleRepository>();

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
