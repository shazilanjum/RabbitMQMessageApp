namespace RabbitMQ.Pub
{
    public interface IPublisher
    {
        Task Send();
    }
}