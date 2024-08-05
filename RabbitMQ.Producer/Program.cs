using RabbitMQ.Client;
using RabbitMQ.Producer;

var factory = new ConnectionFactory
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
var basicMessage = new Message();
var directMessage = new Message();
var topicExchange = new Message();
var headerExchange = new Message();
var fanoutExchange = new Message();

while (true)
{
    basicMessage = BasicPublisher.producedMessage(channel, basicMessage);
    directMessage =  DirectExchangePublisher.producedMessage(channel, directMessage);    
    topicExchange =  TopicExchangePublisher.producedMessage(channel, topicExchange);
    headerExchange = HeaderExchangePublisher.producedMessage(channel, headerExchange);
    fanoutExchange = FanoutExchangePublisher.producedMessage(channel, fanoutExchange);
}


