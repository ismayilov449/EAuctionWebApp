using ESourcing.Products.Data.Interfaces;
using ESourcing.Products.Entities;
using ESourcing.Products.Repositories.Interfaces;
using MongoDB.Driver;

namespace ESourcing.Products.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductContext _context;

        public ProductRepository(IProductContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
                DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<Product> GetById(string id)
        {

            try
            {
                return await _context.Products.Find(i => i.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq(i => i.Category, categoryName);
                return await _context.Products.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                return await _context.Products.Find(i => true).ToListAsync();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq(i => i.Name, name);
                return await _context.Products.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task Post(Product product)
        {
            try
            {
                await _context.Products.InsertOneAsync(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Put(Product product)
        {
            try
            {
                var updResult = await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
                return updResult.IsAcknowledged && updResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
