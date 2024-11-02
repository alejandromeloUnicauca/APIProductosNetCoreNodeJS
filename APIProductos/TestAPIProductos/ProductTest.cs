using APIProductos.Context;
using APIProductos.Controllers;
using APIProductos.DTOs;
using APIProductos.Facade;
using APIProductos.Model;
using APIProductos.Repositories;
using APIProductos.Services;
using EntityFrameworkCore.Testing.Moq;
using EntityFrameworkCore.Testing.Moq.Extensions;
using EntityFrameworkCore.Testing.Moq.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TestAPIProductos.Util;

namespace TestAPIProductos
{
    public class ProductTest
    {

        [Fact]
        public void Test_GET_ALL_ReturnsEmpty()
        {
            var mockedDbContext = Create.MockedDbContextFor<ApplicationDbContext>();
            ProductRepository productRepository = new ProductRepository(mockedDbContext);
            UnitOfWorkRepositories unitOfWorkRepositories = new UnitOfWorkRepositories(mockedDbContext, productRepository);
            Productservice productservice = new Productservice(unitOfWorkRepositories);
            ProductController productController = new ProductController(productservice);

            var result = productController.getAllProducts();
                
            ActionResult<ResponseDTO> actionResult = Assert.IsType<ActionResult<ResponseDTO>>(result.Result);
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>( (OkObjectResult)actionResult.Result);
            ResponseDTO responseDTO = Assert.IsType<ResponseDTO>(objectResult.Value);
            Assert.IsAssignableFrom<IEnumerable<Product>>(responseDTO.data);
            IEnumerable<Product> products = (IEnumerable<Product>)responseDTO.data;
            Assert.Empty(products);
            
        }


        [Fact]
        public void Test_GET_ALL_ReturnsValues()
        {

            var mockedDbContext = Create.MockedDbContextFor<ApplicationDbContext>();
            var product1 = ProductFaker.generate();
            var product2 = ProductFaker.generate();
            var product3 = ProductFaker.generate();
            mockedDbContext.Product.AddRange(product1,product2,product3);
            mockedDbContext.SaveChanges();
            mockedDbContext.Set<Product>().AddFromSqlInterpolatedResult(mockedDbContext.Product.ToList());

            ProductRepository productRepository = new ProductRepository(mockedDbContext);
            UnitOfWorkRepositories unitOfWorkRepositories = new UnitOfWorkRepositories(mockedDbContext, productRepository);
            Productservice productservice = new Productservice(unitOfWorkRepositories);
            ProductController productController = new ProductController(productservice);

            var result = productController.getAllProducts();

            ActionResult<ResponseDTO> actionResult = Assert.IsType<ActionResult<ResponseDTO>>(result.Result);
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>((OkObjectResult)actionResult.Result);
            ResponseDTO responseDTO = Assert.IsType<ResponseDTO>(objectResult.Value);
            Assert.IsAssignableFrom<IEnumerable<Product>>(responseDTO.data);
            IEnumerable<Product> products = (IEnumerable<Product>)responseDTO.data;
            Assert.True(products.Count() == 3);
            
        }

        [Fact]
        public void Test_GET_BY_ID_NOT_FOUND()
        {

            var mockedDbContext = Create.MockedDbContextFor<ApplicationDbContext>();
            mockedDbContext.Set<Product>().AddFromSqlInterpolatedResult(mockedDbContext.Product.ToList());

            ProductRepository productRepository = new ProductRepository(mockedDbContext);
            UnitOfWorkRepositories unitOfWorkRepositories = new UnitOfWorkRepositories(mockedDbContext, productRepository);
            Productservice productservice = new Productservice(unitOfWorkRepositories);
            ProductController productController = new ProductController(productservice);

            Bogus.Faker bogusFaker = new Bogus.Faker();

            var result = productController.getProductById(bogusFaker.Random.Int(1,999999));

            ActionResult<ResponseDTO> actionResult = Assert.IsType<ActionResult<ResponseDTO>>(result.Result);
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>((OkObjectResult)actionResult.Result);
            ResponseDTO responseDTO = Assert.IsType<ResponseDTO>(objectResult.Value);
            Assert.Equal(responseDTO.Code, ((int)HttpStatusCode.NotFound));

        }

        [Fact]
        public void Test_GET_BY_ID_FOUND()
        {

            var mockedDbContext = Create.MockedDbContextFor<ApplicationDbContext>();
            var product1 = ProductFaker.generate();
            var product2 = ProductFaker.generate();
            var product3 = ProductFaker.generate();
            mockedDbContext.AddRange(product1,product2, product3);
            mockedDbContext.SaveChanges();

            Bogus.Faker bogusFaker = new Bogus.Faker();

            int id = bogusFaker.Random.Int(1, 3);
            mockedDbContext.Set<Product>().AddFromSqlInterpolatedResult(mockedDbContext.Product.ToList().FindAll(p=>p.Id == id));

            ProductRepository productRepository = new ProductRepository(mockedDbContext);
            UnitOfWorkRepositories unitOfWorkRepositories = new UnitOfWorkRepositories(mockedDbContext, productRepository);
            Productservice productservice = new Productservice(unitOfWorkRepositories);
            ProductController productController = new ProductController(productservice);

            var result = productController.getProductById(bogusFaker.Random.Int(1, 3));

            ActionResult<ResponseDTO> actionResult = Assert.IsType<ActionResult<ResponseDTO>>(result.Result);
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>((OkObjectResult)actionResult.Result);
            ResponseDTO responseDTO = Assert.IsType<ResponseDTO>(objectResult.Value);
            Assert.Equal(responseDTO.Code, ((int)HttpStatusCode.OK));
            Assert.Equal(((Product)responseDTO.data).Id,id);
        }

        [Fact]
        public void Test_POST_CREATE_PRODUCT_Error()
        {

            var mockedDbContext = Create.MockedDbContextFor<ApplicationDbContext>();
            var product1 = ProductFaker.generate();
            //mockedDbContext.AddRange(product1);
            //mockedDbContext.SaveChanges();

            Bogus.Faker bogusFaker = new Bogus.Faker();

            mockedDbContext.AddExecuteSqlInterpolatedResult(0);

            ProductRepository productRepository = new ProductRepository(mockedDbContext);
            UnitOfWorkRepositories unitOfWorkRepositories = new UnitOfWorkRepositories(mockedDbContext, productRepository);
            Productservice productservice = new Productservice(unitOfWorkRepositories);
            ProductController productController = new ProductController(productservice);

            var result = productController.addProduct(product1);

            ActionResult<ResponseDTO> actionResult = Assert.IsType<ActionResult<ResponseDTO>>(result.Result);
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>((OkObjectResult)actionResult.Result);
            ResponseDTO responseDTO = Assert.IsType<ResponseDTO>(objectResult.Value);
            Assert.Equal(responseDTO.Code, ((int)HttpStatusCode.InternalServerError));
        }
    }
}