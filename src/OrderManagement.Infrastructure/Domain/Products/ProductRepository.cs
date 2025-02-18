using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Products;
using OrderManagement.Infrastructure.Persistence;


namespace OrderManagement.Infrastructure.Domain.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly OrderManagementContext _context;

        public ProductRepository(OrderManagementContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByCodeAsync(string code)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
