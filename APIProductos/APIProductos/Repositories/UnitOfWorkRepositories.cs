using APIProductos.Context;
using APIProductos.Model;
using APIProductos.Repositories;
using APIProductos.Repository;
using Microsoft.EntityFrameworkCore;

namespace APIProductos.Facade
{
    public class UnitOfWorkRepositories : IUnitOfWorkRepositories
    {
        private readonly ApplicationDbContext _dbContext;
        public IProductRepository ProductRepository { get; }
        private bool disposed = false;
        private bool disposedValue;

        public UnitOfWorkRepositories(ApplicationDbContext dbContext, IProductRepository productRepository)
        {
            _dbContext = dbContext;
            ProductRepository = productRepository;
        }

        public async Task Save()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e) { throw e; }
            catch (Exception ex) { throw ex.InnerException; }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
