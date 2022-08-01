using AutoMapper;
using ESouring.Ordering.Application.Commands.OrderCreate;
using EventBusRabbitMQ.Events;

namespace ESourcing.Orders.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderCreateEvent, OrderCreateCommand>().ReverseMap();
        }
    }
}
