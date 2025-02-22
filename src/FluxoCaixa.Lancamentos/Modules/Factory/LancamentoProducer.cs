using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace FluxoCaixa.Consolidado.Modules.Factory
{
    public class LancamentoProducer
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;

        public LancamentoProducer()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost", 
                Port = 5672, 
                UserName = "admin",
                Password = "admin123",
                VirtualHost = "/"
            };

            factory.ClientProvidedName = "app:audit component:lancamentos";

            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();

            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();

            _channel.QueueDeclareAsync(queue: "lancamentos", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public async Task PublicarLancamento(decimal valor, string tipo)
        {
            var lancamento = new { Valor = valor, Tipo = tipo, Data = DateTime.UtcNow };
            
            byte[] body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(lancamento));

            var props = new BasicProperties();
            props.ContentType = "text/plain";
            props.DeliveryMode = DeliveryModes.Persistent;

            await _channel.BasicPublishAsync(exchange: "", routingKey: "lancamentos", mandatory: true, basicProperties: props, body: body);

            Console.WriteLine($"Mensagem enviada: {valor} - {tipo}");
        }

        public void Dispose()
        {
            _channel.CloseAsync();
            _connection.CloseAsync();
        }
    }
}