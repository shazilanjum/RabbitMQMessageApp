using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Pub;

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
channel.QueueDeclare("sub_1", exclusive: false);
//Set Event object which listen message from chanel which is sent by producer
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) => {
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write($"Them: ");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine(message);
};

//read the message
channel.BasicConsume(queue: "sub_1", autoAck: true, consumer: consumer);

while (true)
{

    var message = Console.ReadLine();

    var publisher = new Publisher();
    publisher.Send(message,"sub_2"); //Will send message to appServerA



    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write($"You: ");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine(message);
}