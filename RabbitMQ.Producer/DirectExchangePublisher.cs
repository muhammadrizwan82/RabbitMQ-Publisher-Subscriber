using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    public static class DirectExchangePublisher
    {
        public static Message producedMessage(IModel channel, Message message)
        {
            var ttl = new Dictionary<string, object> {
                {"x-message-ttl",30000}
            };
            channel.ExchangeDeclare("demo-direct-exchange", ExchangeType.Direct, arguments: ttl);

            message.name = "Direct Producer";
            message.message = $"Message sequence {(message.sequence == null ? 1 : message.sequence + 1)}";
            message.messageType = "Direct";
            message.sequence = message.sequence == null ? 1 : message.sequence + 1;

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish("demo-direct-exchange", "account.init", null, body);
            Console.WriteLine($"message qeueue {JsonConvert.SerializeObject(message)} at {DateTime.UtcNow.AddHours(5)}");
            Thread.Sleep(1000);
            return message;
        }
    }
}
