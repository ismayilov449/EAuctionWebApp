using AutoMapper;
using ESourcing.Ordering.Domain.Repositories.Abstract.OrderRepo;
using ESouring.Ordering.Application.Queries.OrderQueries;
using ESouring.Ordering.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESouring.Ordering.Application.Handlers.OrderHandlers
{
    public class GetOrdersByUsernameHandler : IRequestHandler<GetOrdersBySellerUsernameQuery, IEnumerable<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersByUsernameHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersBySellerUsernameQuery request, CancellationToken cancellationToken)
        {
            if (String.IsNullOrWhiteSpace(request.Username))
                throw new ApplicationException("Username can not be null");

            var orderList = await _orderRepository.GetOrdersBySellerUsername(request.Username);

            var response = _mapper.Map<IEnumerable<OrderResponse>>(orderList);

            return response;
        }
    }
}
