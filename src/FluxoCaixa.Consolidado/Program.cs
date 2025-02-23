using FluxoCaixa.Consolidado.Modules.Config;
using FluxoCaixa.Consolidado.Modules.Context;
using FluxoCaixa.Consolidado.Modules.Entity;
using FluxoCaixa.Consolidado.Modules.Factory;
using FluxoCaixa.Consolidado.Modules.Repositories;
using FluxoCaixa.Consolidado.Modules.Services;
using FluxoCaixa.Lancamentos.Modules.Repositories.Base;
using FluxoCaixa.Modules.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration["DefaultConnection"]));

builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddScoped<IRepository<ConsolidadoDiario>, Repository<ConsolidadoDiario>>();
builder.Services.AddScoped<IConsolidadoRepository, ConsolidadoRepository>();
builder.Services.AddScoped<IConsolidadoService, ConsolidadoService>();
 
builder.Services.AddHostedService<ConsolidadoConsumer>();

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(8081);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseMetricServer("/metrics");
app.UseHttpMetrics();

app.MapGet("/", () => "Serviço Consolidado");

app.MapControllers();
app.Run();
