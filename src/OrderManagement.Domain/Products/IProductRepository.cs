using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Products
{
    public interface IProductRepository
    {
        // Create methods
        Task<Product> AddProductAsync(Product product);

        // Read methods
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task<Product> GetProductByCodeAsync(string code);
        Task<int> SaveChangesAsync();
    }
}
