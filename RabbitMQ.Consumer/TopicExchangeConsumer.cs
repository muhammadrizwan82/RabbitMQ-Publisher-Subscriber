using RabbitMQ.Client.Events;
using RabbitMQ.Client; 
using System.Text; 

namespace RabbitMQ.Consumer
{
    public static class TopicExchangeConsumer
    {
        public static void consumeMessage(IModel channel)
        {

            channel.ExchangeDeclare("demo-topic-exchange", ExchangeType.Topic);
            channel.QueueDeclare("demo-topic-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind("demo-topic-queue", "demo-topic-exchange", "account.*");
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Topic consume {message} at {DateTime.UtcNow.AddHours(5)}");
            };

            channel.BasicConsume("demo-topic-queue", autoAck: true, consumer: consumer);
        }
    }
}
