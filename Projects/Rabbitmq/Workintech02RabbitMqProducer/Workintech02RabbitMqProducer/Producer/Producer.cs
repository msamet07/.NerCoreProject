using RabbitMQ.Client;
using Workintech02RabbitMqProducer.Models;

namespace Workintech02RabbitMqProducer.Producer
{
    public class Producer : IProducer
    {
        protected readonly IModel model;

        public Producer()
        {
            var factory = new ConnectionFactory() { HostName = MessageConstant.HOST_NAME };
            var connection = factory.CreateConnection();
            model = connection.CreateModel();

            model.QueueDeclare(queue: MessageConstant.QUEUE_NAME,
                               durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public void SendMessage(string message)
        {
            var messageBody = System.Text.Encoding.UTF8.GetBytes(message);
            model.BasicPublish(exchange: "", routingKey: MessageConstant.QUEUE_NAME, basicProperties: null, body: messageBody);
        }
    }
}
