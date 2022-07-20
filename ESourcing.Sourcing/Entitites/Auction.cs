using ESourcing.Products.Entities.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ESourcing.Sourcing.Entitites
{
    public class Auction
    {
        public Auction()
        {
            IncludedSellers = new List<string>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CraetedAt { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public Status Status { get; set; }
        public List<string> IncludedSellers { get; set; }
    }
}
