using ESourcing.Sourcing.Data.Interfaces;
using ESourcing.Sourcing.Entitites;
using ESourcing.Sourcing.Repositories.Interfaces;
using MongoDB.Driver;

namespace ESourcing.Sourcing.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly ISourcingContext _context;

        public BidRepository(ISourcingContext context)
        {
            _context = context;
        }

        public async Task<List<Bid>> GetBidsByAuctionId(string id)
        {
            FilterDefinition<Bid> filterDefinition = Builders<Bid>.Filter.Eq(b => b.AuctionId, id);
            var res = await _context.Bids.Find(filterDefinition).ToListAsync();

            res = res.OrderByDescending(a => a.CreatedAt)
                     .GroupBy(a => a.SellerUserName)
                     .Select(a => new Bid() 
                     { 
                         AuctionId = a.FirstOrDefault().AuctionId, 
                         Price = a.FirstOrDefault().Price, 
                         CreatedAt = a.FirstOrDefault().CreatedAt, 
                         SellerUserName = a.FirstOrDefault().SellerUserName, 
                         ProductId = a.FirstOrDefault().ProductId, 
                         Id = a.FirstOrDefault().Id 
                     })
                     .ToList();
            return res;
        }

        public async Task<Bid> GetBidWinner(string auctionId)
        {
            var bids = await GetBidsByAuctionId(auctionId);
            return bids.OrderByDescending(a => a.Price).FirstOrDefault();
        }

        public async Task SendBid(Bid bid)
        {
            await _context.Bids.InsertOneAsync(bid);
        }
    }
}
