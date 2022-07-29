using ESourcing.Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESourcing.Ordering.Infrastructure.Data
{
    public class OrderDBContextSeed
    {
        public static async Task SeedAsync(OrderDBContext orderDBContext)
        {
            if (orderDBContext.Orders.Any() is false)
            {
                Order[] orders = new Order[5];

                orders[0] = new Order
                {
                    AuctionId = Guid.NewGuid().ToString(),
                    CreatedDate = DateTime.UtcNow,
                    ProductId = Guid.NewGuid().ToString(),
                    SellerUsername = "tesst 10",
                    TotalPrice = 20,
                    UnitPrice = 2
                };

                orders[1] = new Order
                {
                    AuctionId = Guid.NewGuid().ToString(),
                    CreatedDate = DateTime.UtcNow,
                    ProductId = Guid.NewGuid().ToString(),
                    SellerUsername = "tesst 1",
                    TotalPrice = 20,
                    UnitPrice = 2
                };

                orders[2] = new Order
                {
                    AuctionId = Guid.NewGuid().ToString(),
                    CreatedDate = DateTime.UtcNow,
                    ProductId = Guid.NewGuid().ToString(),
                    SellerUsername = "tesst 2",
                    TotalPrice = 20,
                    UnitPrice = 2
                };

                orders[3] = new Order
                {
                    AuctionId = Guid.NewGuid().ToString(),
                    CreatedDate = DateTime.UtcNow,
                    ProductId = Guid.NewGuid().ToString(),
                    SellerUsername = "tesst 3",
                    TotalPrice = 20,
                    UnitPrice = 2
                };

                orders[4] = new Order
                {
                    AuctionId = Guid.NewGuid().ToString(),
                    CreatedDate = DateTime.UtcNow,
                    ProductId = Guid.NewGuid().ToString(),
                    SellerUsername = "tesst 4",
                    TotalPrice = 20,
                    UnitPrice = 2
                };

                orderDBContext.Orders.AddRange(orders);
                await orderDBContext.SaveChangesAsync();
            }
        }
    }
}
