using AspNetCoreRateLimit;
using Ocelot.DependencyInjection;

namespace ESourcing.ApiGateway.Infrastructure
{
    public static class ProjectDependencies
    {
        public static IServiceCollection AddpProjectDependencies(this IServiceCollection services)
        {

            services.AddOcelot();

            services.AddMemoryCache();

            services.Configure<IpRateLimitOptions>(res =>
            {
                res.EnableEndpointRateLimiting = true;
                res.StackBlockedRequests = false;
                res.HttpStatusCode = 429;
                res.RealIpHeader = "X-Real-IP";
                res.ClientIdHeader = "X-ClientId";
                //res.IpWhitelist = new List<string>
                //{
                //    "127.0.0.1","192.168.1.106"
                //};
                res.GeneralRules = new List<RateLimitRule>()
            {
                new RateLimitRule
                {
                    Endpoint = "*:/Product",
                    Period = "1m",
                    Limit = 20
                },
                new RateLimitRule
                {
                    Endpoint = "*:/Auction",
                    Period = "1m",
                    Limit = 20
                } ,
                new RateLimitRule
                {
                    Endpoint = "*:/Bid",
                    Period = "1m",
                    Limit = 20
                },
                new RateLimitRule
                {
                    Endpoint = "*:/Order",
                    Period = "1m",
                    Limit = 20
                }
            };
            });

            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddInMemoryRateLimiting();

            return services;
        }
    }
}
