using AutoMapper;
using ESourcing.Sourcing.Entitites;
using EventBusRabbitMQ.Events;

namespace ESourcing.Sourcing.Mapping
{
    public class SourcingMaps : Profile
    {
        public SourcingMaps()
        {
            CreateMap<OrderCreateEvent, Bid>().ReverseMap();
        }
    }
}
