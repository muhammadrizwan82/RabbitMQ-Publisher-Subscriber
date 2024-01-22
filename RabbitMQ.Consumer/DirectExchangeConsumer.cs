using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
namespace RabbitMQ.Consumer
{
    public static class DirectExchangeConsumer
    {
        public static void consumeMessage(IModel channel) {

            channel.ExchangeDeclare("demo-direct-exchange", ExchangeType.Direct);
            channel.QueueDeclare("demo-direct-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind("demo-direct-queue", "demo-direct-exchange", "account.init");
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Direct consume {message} at {DateTime.UtcNow.AddHours(5)}");
            };

            channel.BasicConsume("demo-direct-queue", autoAck: true, consumer: consumer);
        }
    }
}
