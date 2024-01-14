using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text; 

namespace RabbitMQ.Producer
{
    public static class Producer
    {
        public static Message producedMessage(IModel channel) {
           
            channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            var message = new Message()
            {
                name = "Producer",
                message = "Hello",
                messageId = DateTime.Now.Ticks.ToString()
            };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish("", "demo-queue", null, body);            
            Console.WriteLine($"message qeueue {JsonConvert.SerializeObject(message)} at {DateTime.UtcNow.AddHours(5)}");
            Thread.Sleep(5000);
            return message;
        }
    }
}
