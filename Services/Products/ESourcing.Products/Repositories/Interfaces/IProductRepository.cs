using ESourcing.Products.Entities;

namespace ESourcing.Products.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetById(string id);
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<IEnumerable<Product>> GetProductByCategory(string categoryName);
        
        Task Post(Product product);
        Task<bool> Put(Product product);  
        Task<bool> Delete(string id); 
    }
}
