 
using RabbitMQ.Client;
using RabbitMQ.Consumer;

var factory = new ConnectionFactory
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

BasicConsumer.consumeMessage(channel);
DirectExchangeConsumer.consumeMessage(channel);
TopicExchangeConsumer.consumeMessage(channel);
HeaderExchangeConsumer.consumeMessage(channel);
FanoutExchangeConsumer.consumeMessage(channel);

Console.ReadLine();