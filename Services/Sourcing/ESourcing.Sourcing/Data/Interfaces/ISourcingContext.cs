using ESourcing.Sourcing.Entitites;
using MongoDB.Driver;

namespace ESourcing.Sourcing.Data.Interfaces
{
    public interface ISourcingContext
    {
        public IMongoCollection<Auction> Auctions { get; }
        public IMongoCollection<Bid> Bids { get; }
    }
}
