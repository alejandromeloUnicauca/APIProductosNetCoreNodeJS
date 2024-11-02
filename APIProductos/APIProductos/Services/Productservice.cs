using APIProductos.Model;
using APIProductos.Repositories;

namespace APIProductos.Services
{
    public class Productservice:IProductService
    {
        private readonly IUnitOfWorkRepositories _unitOfWorkRepositories;

        public Productservice(IUnitOfWorkRepositories unitOfWorkRepositories)
        {
            _unitOfWorkRepositories = unitOfWorkRepositories;
        }

        /// <summary>
        /// Metodo encargado de agregar nuevos productos
        /// </summary>
        /// <param name="product">Objeto que contiene la informacion del producto</param>
        /// <returns>Retorna el identificador del producto creado</returns>
        public async Task<int> addProductAsync(Product product)
        {
            int id = await _unitOfWorkRepositories.ProductRepository.CreateProductAsync(product);
            if (id == -1)
                throw new Exception();
            return id;
        }


        /// <summary>
        /// Obtiene una lista con todos los productos almacenados
        /// </summary>
        /// <returns>Retorna una Lista de objetos de tipo Product</returns>
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWorkRepositories.ProductRepository.GetAll();
        }

        /// <summary>
        /// Busca los productos por id
        /// </summary>
        /// <param name="id">Identificador del producto</param>
        /// <returns>Retorna un objeto con la informacion del producto</returns>
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _unitOfWorkRepositories.ProductRepository.GetById(id);
        }

    }
}
