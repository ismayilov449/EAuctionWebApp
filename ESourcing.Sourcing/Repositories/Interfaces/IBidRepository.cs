using ESourcing.Sourcing.Entitites;

namespace ESourcing.Sourcing.Repositories.Interfaces
{
    public interface IBidRepository
    {
        Task SendBid(Bid bid);
        Task<List<Bid>> GetBidsByAuctionId(string id);
        Task<Bid> GetBidWinner(string auctionId);
    }
}
