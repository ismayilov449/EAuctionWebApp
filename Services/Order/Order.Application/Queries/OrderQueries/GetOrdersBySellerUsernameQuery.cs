using ESouring.Ordering.Application.Responses;
using MediatR;

namespace ESouring.Ordering.Application.Queries.OrderQueries
{
    public class GetOrdersBySellerUsernameQuery : IRequest<IEnumerable<OrderResponse>>
    {
        public string Username { get; set; }
    }
}
