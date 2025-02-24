using System.Text;
using System.Text.Json;
using FluxoCaixa.Lancamentos.Modules.Config;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace FluxoCaixa.Consolidado.Modules.Factory
{
    public interface ILancamentoProducer
    {
        Task PublicarLancamento(decimal valor, string tipo);
    }

    public class LancamentoProducer(IOptions<RabbitMQSettings> rabbitMQSettings) : ILancamentoProducer, IDisposable
    {
        private readonly RabbitMQSettings _rabbitMQSettings = rabbitMQSettings.Value;
        private IConnection? _connection;
        private IChannel? _channel;

        public async Task PublicarLancamento(decimal valor, string tipo)
        {
            await InitializeRabbitMQ();

            var lancamento = new { Valor = valor, Tipo = tipo, Data = DateTime.UtcNow };
            
            byte[] body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(lancamento));

            var props = new BasicProperties
            {
                ContentType = "text/plain",
                DeliveryMode = DeliveryModes.Persistent
            };

            if (_channel != null)
            {
                await _channel.BasicPublishAsync(exchange: "", routingKey: "lancamentos", mandatory: true, basicProperties: props, body: body);
            }

            Dispose();
        }

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

           factory.ClientProvidedName = "app:audit component:lancamentos";

            _connection = await factory.CreateConnectionAsync();

            _channel = await _connection.CreateChannelAsync();

            await _channel.QueueDeclareAsync(queue: "lancamentos", durable: false, exclusive: false, autoDelete: false, arguments: null);
       }

        public void Dispose()
        {
            _channel?.CloseAsync();
            _connection?.CloseAsync();
        }
    }
}