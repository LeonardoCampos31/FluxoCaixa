using System.Text;
using System.Text.Json;
using FluxoCaixa.Consolidado.Modules.Config;
using FluxoCaixa.Consolidado.Modules.Entity;
using FluxoCaixa.Consolidado.Modules.Services;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FluxoCaixa.Consolidado.Modules.Factory
{
    public class ConsolidadoConsumer(IServiceScopeFactory serviceScopeFactory, IOptions<RabbitMQSettings> rabbitMQSettings) : BackgroundService
    {
        private readonly RabbitMQSettings _rabbitMQSettings = rabbitMQSettings.Value;
        private IConnection? _connection;
        private IChannel? _channel;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        private async Task InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMQSettings.HostName,
                Port = _rabbitMQSettings.Port,
                UserName = _rabbitMQSettings.UserName,
                Password = _rabbitMQSettings.Password,
                VirtualHost = _rabbitMQSettings.VirtualHost
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
            await _channel.QueueDeclareAsync(queue: "lancamentos", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await InitializeRabbitMQ();

            if (_channel == null)
            {
                throw new InvalidOperationException("RabbitMQ channel is not initialized.");
            }

            var consumer = new AsyncEventingBasicConsumer(_channel);
            
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var lancamento = JsonSerializer.Deserialize<Lancamento>(message);

                if (lancamento != null)
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var consolidadoService = scope.ServiceProvider.GetRequiredService<IConsolidadoService>();
                    await consolidadoService.AtualizarConsolidadoAsync(lancamento.Data, lancamento.Valor);
                }
            };

            await _channel.BasicConsumeAsync(queue: "lancamentos", autoAck: true, consumer: consumer, cancellationToken: stoppingToken);
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        public override void Dispose()
        {
            _channel?.CloseAsync();
            _connection?.CloseAsync();
            base.Dispose();
        }
    }
}