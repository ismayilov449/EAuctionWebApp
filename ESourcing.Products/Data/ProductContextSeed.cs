using ESourcing.Products.Entities;
using MongoDB.Driver;

namespace ESourcing.Products.Data
{
    public class ProductContextSeed
    {

        public static void SeedData(IMongoCollection<Product> products)
        {
            bool existProduct = products.Find(p => true).Any();

            if (!existProduct)
            {
                products.InsertManyAsync(GetConfigureProducts());
            }
        }

        private static IEnumerable<Product> GetConfigureProducts()
        {
            return new List<Product>() {
                new Product
                {
                    Name = "IPhone 11",
                    Summary = "Pechat phone",
                    Description = "Lorem ipsum da qaqa",
                    ImageUrl = "null",
                    Price = 1599M,
                    Category = "Smartphone"
                },
                new Product
                {
                    Name = "IPhone 13 Pro Max",
                    Summary = "Pechat deyil",
                    Description = "Lorem ipsum da qaqa",
                    ImageUrl = "null",
                    Price = 3599M,
                    Category = "Smartphone"
                },
                new Product
                {
                    Name = "IPhone 5s",
                    Summary = "Legend phone",
                    Description = "Lorem ipsum da qaqa",
                    ImageUrl = "null",
                    Price = 259M,
                    Category = "Smartphone"
                },
                new Product
                {
                    Name = "IPhone X",
                    Summary = "X man phone",
                    Description = "Lorem ipsum da qaqa",
                    ImageUrl = "null",
                    Price = 1599M,
                    Category = "Smartphone"
                },
                new Product
                {
                    Name = "Samsung",
                    Summary = "Pechat phone",
                    Description = "Lorem ipsum da qaqa",
                    ImageUrl = "null",
                    Price = 599M,
                    Category = "Smartphone"
                },

            };
        }
    }
}
