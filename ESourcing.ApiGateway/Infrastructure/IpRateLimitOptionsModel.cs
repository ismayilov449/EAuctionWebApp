using AspNetCoreRateLimit;

namespace ESourcing.ApiGateway.Infrastructure
{
    public static class IpRateLimitOptionsModel
    {
        public static IpRateLimitOptions Get()
        {
            var res = new IpRateLimitOptions();

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
                    Period = "5s",
                    Limit = 2
                },
                new RateLimitRule
                {
                    Endpoint = "*:/Auction",
                    Period = "1m",
                    Limit = 10
                } ,
                new RateLimitRule
                {
                    Endpoint = "*:/Bid",
                    Period = "1m",
                    Limit = 10
                },
                new RateLimitRule
                {
                    Endpoint = "*:/Order",
                    Period = "1m",
                    Limit = 10
                }
            };

            return res;
        }

    }
}
