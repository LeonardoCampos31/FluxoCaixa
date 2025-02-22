using System.Text;
using System.Text.Json;
using FluxoCaixa.Consolidado.Modules.Entity;
using FluxoCaixa.Consolidado.Modules.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FluxoCaixa.Consolidado.Modules.Factory
{
    public class ConsolidadoConsumer : BackgroundService
    {
        private IConnection _connection;
        private IChannel _channel;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ConsolidadoConsumer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        private async Task InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "admin",
                Password = "admin123",
                VirtualHost = "/"
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
            await _channel.QueueDeclareAsync(queue: "lancamentos", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await InitializeRabbitMQ();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var lancamento = JsonSerializer.Deserialize<Lancamento>(message);

                if (lancamento != null)
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var consolidadoService = scope.ServiceProvider.GetRequiredService<ConsolidadoService>();
                        await consolidadoService.AtualizarConsolidadoAsync(lancamento.Data, lancamento.Valor);
                    }
                }
            };

            await _channel.BasicConsumeAsync(queue: "lancamentos", autoAck: true, consumer: consumer);
            await Task.Delay(Timeout.Infinite, stoppingToken);
            //Dispose();
        }

        public override void Dispose()
        {
            _channel?.CloseAsync();
            _connection?.CloseAsync();
            base.Dispose();
        }
    }
}