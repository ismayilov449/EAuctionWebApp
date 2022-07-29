using ESourcing.Products.Entities.Enums;
using ESourcing.Sourcing.Entitites;
using MongoDB.Driver;

namespace ESourcing.Sourcing.Data
{
    public class SourcingContextSeed
    {
        public static void SeedData(IMongoCollection<Auction> auctions, IMongoCollection<Bid> bids)
        {
            bool existAuction = auctions.Find(p => true).Any();

            if (!existAuction)
            {
                auctions.InsertManyAsync(GetConfigureAuctions());
            }

            bool existBid = bids.Find(p => true).Any();

            if (!existBid)
            {
                bids.InsertManyAsync(GetConfigureBids());
            }

        }

        private static IEnumerable<Auction> GetConfigureAuctions()
        {
            return new List<Auction>()
            {
                new Auction()
                {
                    CraetedAt = DateTime.Now,
                    FinishedAt = DateTime.Now.AddDays(1),
                    Description = "Zor",
                    StartedAt = DateTime.Now.AddHours(1),
                    IncludedSellers = new List<string>{ "rufat@gmail.com","mammad@gmail.com"},
                    Name = "IPhone 11",
                    ProductId = "62d6aea575361dc52f23e289",
                    Quantity = 1,
                    Status = (int)Status.Active,
                    Id = "33d6aaa555361dc52f23e289"
                },
                new Auction()
                {
                    CraetedAt = DateTime.Now,
                    FinishedAt = DateTime.Now.AddDays(1),
                    Description = "Zor 2",
                    StartedAt = DateTime.Now.AddHours(1),
                    IncludedSellers = new List<string>(){ "urfan@gmail.com"},
                    Name = "IPhone 13 Pro Max",
                    ProductId = "62d6aea575361dc52f23e28a",
                    Quantity = 1,
                    Status = (int)Status.Active,
                    Id = "44d6aaa555361dc52f23e289"
                }
            };
        }

        private static IEnumerable<Bid> GetConfigureBids()
        {
            return new List<Bid>()
            {
                new Bid()
                {
                    AuctionId = "33d6aaa555361dc52f23e289",
                    CreatedAt = DateTime.Now,
                    Price = 500M,
                    ProductId = "62d6aea575361dc52f23e289",
                    SellerUserName = "Rufat"
                },
                new Bid()
                {
                    AuctionId = "33d6aaa555361dc52f23e289",
                    CreatedAt = DateTime.Now,
                    Price = 400M,
                    ProductId = "62d6aea575361dc52f23e289",
                    SellerUserName = "Mammad"
                },
                new Bid()
                {
                    AuctionId = "44d6aaa555361dc52f23e289",
                    CreatedAt = DateTime.Now,
                    Price = 300M,
                    ProductId = "62d6aea575361dc52f23e28a",
                    SellerUserName = "Urfan"
                },
            };
        }
    }
}
