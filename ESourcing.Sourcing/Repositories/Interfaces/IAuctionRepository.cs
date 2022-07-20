using ESourcing.Sourcing.Entitites;

namespace ESourcing.Sourcing.Repositories.Interfaces
{
    public interface IAuctionRepository
    {
        Task<IEnumerable<Auction>> GetAll();
        Task<Auction> GetById(string id);
        Task<Auction> GetByName(string name);

        Task Post(Auction auction);
        Task<bool> Put(Auction auction);
        Task<bool> Delete(string id);
    }
}
