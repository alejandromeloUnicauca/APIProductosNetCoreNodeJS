using APIProductos.DTOs;
using APIProductos.Model;
using APIProductos.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APIProductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Servicio para obtener los productos almacenados
        /// </summary>
        /// <returns>Retorna una lista de productos</returns>
        [HttpGet(template:"AllProducts")]
        public async Task<ActionResult<ResponseDTO>> getAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(new ResponseDTO() { Code = ((int)HttpStatusCode.OK), Message = MensajesResponse.Success, data = products }); 
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO() { Code = ((int)HttpStatusCode.InternalServerError), Message = MensajesResponse.Error});
            }
        }

        ///// <summary>
        ///// Servicio para buscar los productos por identificador
        ///// </summary>
        ///// <param name="id">Identificador del producto</param>
        ///// <returns>Retorna un objeto con la informacion del producto</returns>
        [HttpGet(template:"GetProductById/{id}")]
        public async Task<ActionResult<ResponseDTO>> getProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if(product == null) 
                    return Ok(new ResponseDTO() { Code = ((int)HttpStatusCode.NotFound), Message = MensajesResponse.NotFound});
                return Ok(new ResponseDTO() { Code = ((int)HttpStatusCode.OK), Message = MensajesResponse.Success, data = product });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO() { Code = 500, Message = MensajesResponse.Error });
            }
        }

        /// <summary>
        /// Servicio para crear productos
        /// </summary>
        /// <param name="product">Objeto con la informacion de los productos</param>
        /// <returns>Retorna el identificador del producto creado</returns>
        [HttpPost(template:"Add")]
        public async Task<ActionResult<ResponseDTO>> addProduct(Product product)
        {
            try
            {
                var id = await _productService.addProductAsync(product);
                return Ok(new ResponseDTO() { Code = ((int)HttpStatusCode.Created), Message = MensajesResponse.Success, data = id});
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO() { Code = ((int)HttpStatusCode.InternalServerError), Message = MensajesResponse.Error });
            }
        }
    }
}
