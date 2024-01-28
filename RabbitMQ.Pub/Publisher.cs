using System.Text;
using System.Threading.Channels;
using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Pub
{
    public class Publisher
    {
        
        public async Task Send(string message, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            //Here we create channel with session and model
            using
            var channel = connection.CreateModel();
            //declare the queue after mentioning name and a few property related to that
            channel.QueueDeclare(queueName, exclusive: false);
            //Serialize the message
            var body = Encoding.UTF8.GetBytes(message);
            //put the data on to the product queue
            channel.BasicPublish(exchange: "", routingKey: queueName, body: body);
        }
    }
}