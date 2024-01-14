using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Consumer
{
    public static class Consumer
    {
        public static void Consume(IModel channel) {
            channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"message consume {message} at {DateTime.UtcNow.AddHours(5)}");
            };

            channel.BasicConsume("demo-queue", autoAck: true, consumer: consumer);
        }
    }
}
