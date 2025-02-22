using FluxoCaixa.Consolidado.Modules.Factory;
using FluxoCaixa.Lancamentos.Modules.Config;
using FluxoCaixa.Lancamentos.Modules.DataTransfers.Context;
using FluxoCaixa.Lancamentos.Modules.Models;
using FluxoCaixa.Lancamentos.Modules.Repositories;
using FluxoCaixa.Lancamentos.Modules.Repositories.Base;
using FluxoCaixa.Lancamentos.Modules.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration["DefaultConnection"]));

builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddScoped<IRepository<Lancamento>, Repository<Lancamento>>();
builder.Services.AddScoped<ILancamentoRepository, LancamentoRepository>();
builder.Services.AddScoped<LancamentoService>();
builder.Services.AddScoped<LancamentoProducer>();

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));

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
