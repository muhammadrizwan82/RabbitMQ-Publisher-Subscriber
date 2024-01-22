using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    public static class HeaderExchangePublisher
    {
        //https://www.youtube.com/watch?v=EtTPtnn6uKE
        public static Message producedMessage(IModel channel, Message message)
        {
            var ttl = new Dictionary<string, object> {
                {"x-message-ttl",30000}
            };
            channel.ExchangeDeclare("demo-header-exchange", ExchangeType.Headers, arguments: ttl);

            message.name = "Header Producer";
            message.message = $"Message sequence {(message.sequence == null ? 1 : message.sequence + 1)}";
            message.messageType = "Header";
            message.sequence = message.sequence == null ? 1 : message.sequence + 1;
            var properties = channel.CreateBasicProperties();
            properties.Headers = new Dictionary<string, object> {{ "account","update" } };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish("demo-header-exchange", "", properties, body);
            Console.WriteLine($"message qeueue {JsonConvert.SerializeObject(message)} at {DateTime.UtcNow.AddHours(5)}");
            Thread.Sleep(1000);
            return message;
        }
    }
}
