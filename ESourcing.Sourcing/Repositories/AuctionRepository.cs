using ESourcing.Sourcing.Data.Interfaces;
using ESourcing.Sourcing.Entitites;
using ESourcing.Sourcing.Repositories.Interfaces;
using MongoDB.Driver;

namespace ESourcing.Sourcing.Repositories
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly ISourcingContext _context;

        public AuctionRepository(ISourcingContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                var filter = Builders<Auction>.Filter.Eq(p => p.Id, id);
                DeleteResult deleteResult = await _context.Auctions.DeleteOneAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Auction>> GetAll()
        {
            try
            {
                return await _context.Auctions.Find(i => true).ToListAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Auction> GetById(string id)
        {
            try
            {
                return await _context.Auctions.Find(i => i.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Auction> GetByName(string name)
        {
            try
            {
                return await _context.Auctions.Find(i => i.Name == name).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Post(Auction auction)
        {
            try
            {
                await _context.Auctions.InsertOneAsync(auction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Put(Auction auction)
        {
            try
            {
                var updResult = await _context.Auctions.ReplaceOneAsync(filter: g => g.Id == auction.Id, replacement: auction);
                return updResult.IsAcknowledged && updResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
