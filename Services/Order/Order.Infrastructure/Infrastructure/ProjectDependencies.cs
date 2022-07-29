using ESourcing.Ordering.Domain.Repositories;
using ESourcing.Ordering.Domain.Repositories.Abstract.OrderRepo;
using ESourcing.Ordering.Domain.Repositories.Base;
using ESourcing.Ordering.Infrastructure.Data;
using ESourcing.Ordering.Infrastructure.Repositories.Base;
using ESourcing.Ordering.Infrastructure.Repositories.Concrete.OrderRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESourcing.Ordering.Infrastructure.Infrastructure
{
    public static class ProjectDependenciesInfrastructure
    {
        public static IServiceCollection AddInfrastuctureDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<OrderDBContext>(opt => opt.UseInMemoryDatabase("InMemoryDb"), ServiceLifetime.Singleton, ServiceLifetime.Singleton);

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IOrderRepository, OrderRepesitory>();

            return services;
        }
    }
}
