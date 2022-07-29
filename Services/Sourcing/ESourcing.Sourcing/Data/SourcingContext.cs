using ESourcing.Sourcing.Data.Interfaces;
using ESourcing.Sourcing.Entitites;
using ESourcing.Sourcing.Settings;
using MongoDB.Driver;

namespace ESourcing.Sourcing.Data
{
    public class SourcingContext : ISourcingContext
    {
        public SourcingContext(ISourcingDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Auctions = database.GetCollection<Auction>("Auctions");
            Bids = database.GetCollection<Bid>("Bids");

            SourcingContextSeed.SeedData(Auctions, Bids);
        }

        public IMongoCollection<Auction> Auctions { get; }
        public IMongoCollection<Bid> Bids { get; }
    }
}
