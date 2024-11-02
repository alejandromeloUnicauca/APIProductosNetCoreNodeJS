using APIProductos.Model;
using APIProductos.Repository;

namespace APIProductos.Repositories
{
    public interface IUnitOfWorkRepositories: IDisposable
    {
        public IProductRepository ProductRepository { get; }
        Task Save();
    }
}
