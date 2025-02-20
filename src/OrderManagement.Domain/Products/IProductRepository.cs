namespace OrderManagement.Domain.Products
{
    public interface IProductRepository
    {
        Task<Product> AddProductAsync(Product product);

        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task<Product> GetProductByCodeAsync(string code);
        Task<int> SaveChangesAsync();
    }
}
