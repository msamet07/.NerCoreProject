using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reflection;
using System.Text;
using System.Threading.Channels;
using Workintech02RabbitmqConsumer.Models;

namespace Workintech02RabbitmqConsumer.Consumer
{
    public class MessageConsumer : IHostedService
    {
        private readonly IModel model;

        public MessageConsumer()
        {
            var factory = new ConnectionFactory() { HostName = MessageConstant.HOST_NAME };
            var connection = factory.CreateConnection();
            model = connection.CreateModel();

            model.QueueDeclare(queue: MessageConstant.QUEUE_NAME,
                               durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            StartConsuming();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void StartConsuming()
        {
            Console.WriteLine(" [Webapi] Waiting for messages.");
            var consumer = new EventingBasicConsumer(model);
            object messageResponse = null;

            consumer.Received += (messageModel, ea) =>
            {
               var messageBody = ea.Body.ToArray();
               var message = Encoding.UTF8.GetString(messageBody);
               messageResponse = message;
               Console.WriteLine($" [Webapi] Received {message}");
            };
            model.BasicConsume(queue: MessageConstant.QUEUE_NAME, autoAck: true, consumer: consumer);
        }

    }
}
