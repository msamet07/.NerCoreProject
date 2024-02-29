namespace Workintech02RabbitMqProducer.Producer
{
    public interface IProducer
    {
        void SendMessage(string message);
    }
}