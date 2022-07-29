using ESourcing.Ordering.Domain.Entities;
using ESourcing.Ordering.Domain.Repositories;
using ESourcing.Ordering.Domain.Repositories.Abstract.OrderRepo;
using ESourcing.Ordering.Infrastructure.Data;
using ESourcing.Ordering.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESourcing.Ordering.Infrastructure.Repositories.Concrete.OrderRepo
{
    public class OrderRepesitory : Repository<Order>, IOrderRepository
    {
        public OrderRepesitory(OrderDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersBySellerUsername(string userName)
        {
            return await _dbContext.Orders.Where(i => i.SellerUsername == userName).ToListAsync();
        }
    }
}
