using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESouring.Ordering.Application.Responses
{
    public class OrderResponse
    {

        public string Id { get; set; }
        public string AuctionId { get; set; }
        public string SellerUsername { get; set; }
        public string ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
