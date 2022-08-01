using AutoMapper;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Core;
using RabbitMQ.Client;
using MediatR;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using EventBusRabbitMQ.Events;
using ESouring.Ordering.Application.Commands.OrderCreate;

namespace ESourcing.Orders.Consumers
{
    public class EventBusOrderCreateConsumer
    {
        private readonly IRabbitMQPersistentConnection _connection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EventBusOrderCreateConsumer(IRabbitMQPersistentConnection connection, IMediator mediator, IMapper mapper)
        {
            _connection = connection;
            _mediator = mediator;
            _mapper = mapper;
        }

        public void Consume()
        {
            if (!_connection.IsConnected)
            {
                _connection.TryConnect();
            }

            var channel = _connection.CreateModel();

            channel.QueueDeclare(EventBusConstants.OrderCreateQueue, false, false, false, null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += RecievedEvent;


            channel.BasicConsume(EventBusConstants.OrderCreateQueue, true, consumer);

        }

        private async void RecievedEvent(object? sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body.Span);
            var @event = JsonConvert.DeserializeObject<OrderCreateEvent>(message);

            if (e.RoutingKey == EventBusConstants.OrderCreateQueue)
            {
                var command = _mapper.Map<OrderCreateCommand>(@event);

                command.CreatedDate = DateTime.UtcNow;
                command.TotalPrice = @event.Price * @event.Quantity;
                command.UnitPrice = @event.Price;

                await _mediator.Send(command);
            }

        }

        public void Disconnect()
        {
            _connection.Dispose();
        }

    }
}
