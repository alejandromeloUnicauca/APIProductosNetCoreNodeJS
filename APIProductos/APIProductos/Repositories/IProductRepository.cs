using APIProductos.Model;
using APIProductos.Repository;

namespace APIProductos.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<int> CreateProductAsync(Product product);
    }
}
