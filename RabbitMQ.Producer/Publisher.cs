using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    public static class BasicPublisher
    {
        public static Message producedMessage(IModel channel, Message message)
        {

            channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            message.name = "Basic Producer";
            message.message = $"Message sequence {(message.sequence == null ? 1 : message.sequence + 1)}";
            message.messageType = "Basic";
            message.sequence = message.sequence == null ? 1 : message.sequence + 1;


            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish("", "demo-queue", null, body);
            Console.WriteLine($"message qeueue {JsonConvert.SerializeObject(message)} at {DateTime.UtcNow.AddHours(5)}");
            Thread.Sleep(1000);
            return message;
        }
    }
}
