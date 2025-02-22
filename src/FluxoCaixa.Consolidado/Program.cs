using FluxoCaixa.Consolidado.Modules.Context;
using FluxoCaixa.Consolidado.Modules.Entity;
using FluxoCaixa.Consolidado.Modules.Factory;
using FluxoCaixa.Consolidado.Modules.Repositories;
using FluxoCaixa.Consolidado.Modules.Services;
using FluxoCaixa.Lancamentos.Modules.Repositories.Base;
using FluxoCaixa.Modules.Repositories.Base;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddScoped<IRepository<ConsolidadoDiario>, Repository<ConsolidadoDiario>>();
builder.Services.AddScoped<IConsolidadoRepository, ConsolidadoRepository>();
builder.Services.AddScoped<ConsolidadoService>();
builder.Services.AddHostedService<ConsolidadoConsumer>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
