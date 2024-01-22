using Newtonsoft.Json;
using RabbitMQ.Client; 
using System.Text; 

namespace RabbitMQ.Producer
{
    public static class FanoutExchangePublisher
    {
        //https://www.youtube.com/watch?v=EtTPtnn6uKE
        public static Message producedMessage(IModel channel, Message message)
        {
            var ttl = new Dictionary<string, object> {
                {"x-message-ttl",30000}
            };
            channel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout, arguments: ttl);

            message.name = "Fanout Producer";
            message.message = $"Message sequence {(message.sequence == null ? 1 : message.sequence + 1)}";
            message.messageType = "Fanout";
            message.sequence = message.sequence == null ? 1 : message.sequence + 1;
            var properties = channel.CreateBasicProperties();
            properties.Headers = new Dictionary<string, object> {{ "account","new" } };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish("demo-fanout-exchange", "", properties, body);
            Console.WriteLine($"message qeueue {JsonConvert.SerializeObject(message)} at {DateTime.UtcNow.AddHours(5)}");
            Thread.Sleep(1000);
            return message;
        }
    }
}
