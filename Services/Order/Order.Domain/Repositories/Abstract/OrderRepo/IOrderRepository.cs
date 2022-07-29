using ESourcing.Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESourcing.Ordering.Domain.Repositories.Base;

namespace ESourcing.Ordering.Domain.Repositories.Abstract.OrderRepo
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersBySellerUsername(string userName);
    }
}
