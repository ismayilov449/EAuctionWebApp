using AutoMapper;
using ESouring.Ordering.Application.Mapper;
using ESouring.Ordering.Application.PipelineBehaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ESouring.Ordering.Application.Infrastructure
{
    public static class ProjectDependenciesApplication
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            #region Configure Mapper
            var config = new MapperConfiguration(conf =>
            {
                conf.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                conf.AddProfile<OrderMappingProfile>();
            });

            var mapper = config.CreateMapper();
            #endregion

            return services;
        }
    }
}
