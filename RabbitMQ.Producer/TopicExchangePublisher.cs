using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    public static class TopicExchangePublisher
    {
        public static Message producedMessage(IModel channel, Message message)
        {
            var ttl = new Dictionary<string, object> {
                {"x-message-ttl",30000}
            };
            channel.ExchangeDeclare("demo-topic-exchange", ExchangeType.Topic, arguments: ttl);

            message.name = "Topic Producer";
            message.message = $"Message sequence {(message.sequence == null ? 1 : message.sequence + 1)}";
            message.messageType = "Topic";
            message.sequence = message.sequence == null ? 1 : message.sequence + 1;

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish("demo-topic-exchange", "user.update", null, body);
            Console.WriteLine($"message qeueue {JsonConvert.SerializeObject(message)} at {DateTime.UtcNow.AddHours(5)}");
            Thread.Sleep(1000);
            return message;
        }
    }
}
