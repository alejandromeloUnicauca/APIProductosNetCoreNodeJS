using APIProductos.Context;
using APIProductos.Model;
using APIProductos.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace APIProductos.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }

        /// <summary>
        /// Invoca al sp encargado de insertar los productos en base de datos
        /// </summary>
        /// <param name="product">Objeto que contiene la informacion del producto</param>
        /// <returns>Retorna el id del producto insertado</returns>
        public async Task<int> CreateProductAsync(Product product)
        {
            try
            {
                var productIdParameter = new SqlParameter("@ProductId", SqlDbType.Int);
                productIdParameter.Direction = ParameterDirection.Output;

                // Llamada al procedimiento almacenado usando ExecuteSqlInterpolated
                await dbContext.Database.ExecuteSqlInterpolatedAsync($"EXEC CreateProduct @Name={product.Name}, @Description={product.Description}, @Price={product.Price}, @Stock={product.Stock}, @ProductId={productIdParameter} OUTPUT");

                // Retorna el ID del producto creado
                return productIdParameter.Value == null ? -1 : (int)productIdParameter.Value;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        

        /// <summary>
        /// Invoca al SP que consulta los productos por Id
        /// </summary>
        /// <param name="id">Identificador del producto</param>
        /// <returns>Retorna un Objeto de tipo Product que corresponde al id</returns>
        public override async Task<Product> GetById(int id)
        {
            try
            {
                IEnumerable<Product> products = await dbSet.FromSqlInterpolated($"EXEC SearchProduct @Id = {id}").ToListAsync();
                return products.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Invoca al SP que consulta los productos de base de datos
        /// </summary>
        /// <returns>Retorna una lista con los registros de base de datos</returns>
        public override async Task<IEnumerable<Product>> GetAll()
        {
            try
            {
                return await dbSet.FromSqlInterpolated($"EXEC SearchProduct").ToListAsync();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Product>();
            }
        }
    }
}
