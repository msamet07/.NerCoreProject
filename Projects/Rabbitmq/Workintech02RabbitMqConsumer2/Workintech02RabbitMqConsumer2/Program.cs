// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

Console.WriteLine("Hello, World!");
var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "workintech02-queue",
                    durable: true, exclusive: false, autoDelete: false, arguments: null);

Console.WriteLine(" [ConsoleApplication] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [ConsoleApplication] Received {message}");
};
channel.BasicConsume(queue: "workintech02-queue",
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();