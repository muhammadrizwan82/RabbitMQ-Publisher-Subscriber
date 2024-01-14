using RabbitMQ.Client;
using RabbitMQ.Producer;

var factory = new ConnectionFactory
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

while (true)
{    
    Task t = Task.Run(() =>
    {
       Producer.producedMessage(channel);
    });    
    t.Wait();
    
}


