using ESourcing.Ordering.Domain.Entities.Base;

namespace ESourcing.Ordering.Domain.Entities
{
    public class Order : Entity
    {
        public string AuctionId { get; set; }
        public string SellerUsername { get; set; }
        public string ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
