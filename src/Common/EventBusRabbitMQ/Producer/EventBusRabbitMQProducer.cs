using Events.EventBusRabbitMQ;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ.Producer
{
    public class EventBusRabbitMQProducer
    {
        private readonly IRabbitMQConnection _connection;

        public EventBusRabbitMQProducer(IRabbitMQConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }
        public void PublishBasketCheckout(string queuename,BasketCheckOutEvent publishModel)
        {
            using (var channel=_connection.CreatModel())
            {
                channel.QueueDeclare(queue: queuename, durable: false, autoDelete: false, arguments: null);
                var message = JsonConvert.SerializeObject(publishModel);
                var body = Encoding.UTF8.GetBytes(message);

                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.DeliveryMode = 2;

                channel.ConfirmSelect();
                channel.BasicPublish(exchange: "", routingKey: queuename, mandatory: true, basicProperties: properties, body: body);
                channel.WaitForConfirmsOrDie();
                channel.BasicAcks += (sender, eventArgs) =>
                {
                    Console.WriteLine("sent rabbitMQ");
                };
                channel.ConfirmSelect();
            } 
        }
    }
}
