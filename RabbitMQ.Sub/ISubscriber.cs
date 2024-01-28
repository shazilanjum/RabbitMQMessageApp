namespace RabbitMQ.Sub1
{
    public interface ISubscriber
    {
        Task<string> Receive();
    }
}