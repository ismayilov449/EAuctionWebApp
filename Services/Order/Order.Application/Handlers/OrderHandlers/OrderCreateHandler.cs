using AutoMapper;
using ESourcing.Ordering.Domain.Entities;
using ESourcing.Ordering.Domain.Repositories.Abstract.OrderRepo;
using ESouring.Ordering.Application.Commands.OrderCreate;
using ESouring.Ordering.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESouring.Ordering.Application.Handlers.OrderHandlers
{
    public class OrderCreateHandler : IRequestHandler<OrderCreateCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderCreateHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponse> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);

            if (orderEntity is null)
                throw new ApplicationException("Entity could not be mapped");

            var order = await _orderRepository.PostAsync(orderEntity);

            var orderResponse = _mapper.Map<OrderResponse>(order);

            return orderResponse;
        }
    }
}
