using ESourcing.Ordering.Domain.Entities;
using ESourcing.Ordering.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ESourcing.Orders.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDBContext>();

                    if (!dbContext.Database.IsInMemory())
                    {
                        dbContext.Database.Migrate();
                    }

                    OrderDBContextSeed.SeedAsync(dbContext).Wait();

                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            return host;
        }

    }
}
