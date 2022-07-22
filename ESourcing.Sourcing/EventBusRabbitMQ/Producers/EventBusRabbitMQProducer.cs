using EventBusRabbitMQ.Events.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMQ.Producers
{
    public class EventBusRabbitMQProducer
    {
        private readonly IRabbitMQPersistentConnection _connection;
        private readonly ILogger<EventBusRabbitMQProducer> _logger;
        private readonly int _retryCount;

        public EventBusRabbitMQProducer(IRabbitMQPersistentConnection connection, ILogger<EventBusRabbitMQProducer> logger, int retryCount = 5)
        {
            _connection = connection;
            _logger = logger;
            _retryCount = retryCount;
        }

        public void Publish(string queueName, IEvent @event)
        {
            if (_connection.IsConnected)
            {
                _connection.TryConnect();
            }

            var policy = RetryPolicy.Handle<SocketException>()
                        .Or<BrokerUnreachableException>()
                        .WaitAndRetry(_retryCount,
                            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                                (ex, time) =>
                                {
                                    _logger.LogWarning(ex, "RabbitMQ Client could not connect after {TimeOut}s                  " +
                                                "({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex.Message);
                                }
                                     );

            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queueName, false, false, false, null);

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                policy.Execute(() =>
                {
                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    properties.DeliveryMode = 2;

                    channel.ConfirmSelect();
                    channel.BasicPublish(
                        exchange: "",
                        routingKey: queueName,
                        mandatory: true,
                        basicProperties: properties,
                        body: body
                        );
                    channel.WaitForConfirmsOrDie();

                    channel.BasicAcks += (sender, evenArgs) =>
                    {
                        Console.WriteLine("Sent RabbitMQ");
                    };

                });

            }
        }
    }
}
