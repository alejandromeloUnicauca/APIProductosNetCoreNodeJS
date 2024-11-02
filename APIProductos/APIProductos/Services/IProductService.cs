using APIProductos.Model;

namespace APIProductos.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<int> addProductAsync(Product product);

    }
}
