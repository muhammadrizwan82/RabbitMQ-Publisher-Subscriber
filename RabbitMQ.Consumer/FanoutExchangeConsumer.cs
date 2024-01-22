using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public static class FanoutExchangeConsumer
    {
        public static void consumeMessage(IModel channel)
        {

            channel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout);
            channel.QueueDeclare("demo-fanout-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            var header = new Dictionary<string, object> {
                { "account","new"}
            };
            channel.QueueBind("demo-fanout-queue", "demo-fanout-exchange", "");
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Fanout consume {message} at {DateTime.UtcNow.AddHours(5)}");
            };

            channel.BasicConsume("demo-fanout-queue", autoAck: true, consumer: consumer);
        }
    }
}
