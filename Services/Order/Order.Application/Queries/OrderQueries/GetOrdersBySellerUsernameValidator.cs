using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESouring.Ordering.Application.Queries.OrderQueries
{
    public class GetOrdersBySellerUsernameValidator : AbstractValidator<GetOrdersBySellerUsernameQuery>
    {
        public GetOrdersBySellerUsernameValidator()
        {
            RuleFor(c => c.Username).NotEmpty();
        }
    }
}
