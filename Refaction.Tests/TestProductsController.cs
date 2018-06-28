using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refaction.Controllers;
using Refaction.Models;
using Refaction.Dtos;
using Refaction.Data;
using System.Web.Http.Results;
using System.Web.Http;
using System.Net.Http;
using System.Data.Entity;
using System.Collections.Generic;

namespace Refaction.Tests
{
    [TestClass]
    public class TestProductsController
    {

        [TestMethod]
        public void GetAllProducts_ShouldReturnAllProducts()
        {

            var controller = new ProductsController();
            var result = controller.GetProducts();

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Create_NewProduct_ShouldReturnOk()
        {
            var controller = new ProductsController();

            var productDto = new ProductDto
            {
                Id = Guid.NewGuid(),
                Name = "New Product",
                Description = "Add new product.",
                Price = new decimal(123.45),
                DeliveryPrice = new decimal(67.89)
            };

            var result = controller.CreateProduct(productDto);
            controller.DeleteProduct(productDto.Id);

            Assert.IsInstanceOfType(result, typeof(OkResult));

        }

        [TestMethod]
        public void Create_ExistingProduct_ShouldReturnConflictResult()
        {
            var controller = new ProductsController();

            var productDto = new ProductDto
            {
                Name = "Test Existing Product",
                Description = "This product already exists.",
                Price = new decimal(123.45),
                DeliveryPrice = new decimal(67.89)
            };

            controller.CreateProduct(productDto);

            var result = controller.CreateProduct(productDto);
            controller.DeleteProduct(productDto.Id);

            Assert.IsInstanceOfType(result, typeof(ConflictResult));

        }

        [TestMethod]
        public void Update_ExistingProduct_ShouldReturnOK()
        {

            var controller = new ProductsController();

            var productDto = new ProductDto
            {
                Name = "Test Updating Existing Product",
                Description = "This product will be upated.",
                Price = new decimal(123.45),
                DeliveryPrice = new decimal(67.89)
            };

            controller.CreateProduct(productDto);

            var result = controller.UpdateProduct(productDto.Id, productDto);
            controller.DeleteProduct(productDto.Id);
            
            Assert.IsInstanceOfType(result, typeof(OkResult));

        }

        [TestMethod]
        public void Update_NoExistingProduct_ShouldReturnOK()
        {

            var controller = new ProductsController();

            var productDto = new ProductDto
            {
                Name = "Test Updating Existing Product",
                Description = "This product will be upated.",
                Price = new decimal(123.45),
                DeliveryPrice = new decimal(67.89)
            };

            var result = controller.UpdateProduct(productDto.Id, productDto);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void Delete_NoExistingProduct_ShouldReturnNotFound()
        {
            var controller = new ProductsController();
            var result = controller.DeleteProduct(Guid.NewGuid());

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void Delete_ExistingProduct_ShouldReturnOk()
        {

            var controller = new ProductsController();
            var context = new ApplicationDbContext();

            var productDto = new ProductDto
            {
                Id = Guid.NewGuid(),
                Name = "Test Product to delete ",
                Description = "Add new product to delete.",
                Price = new decimal(123.45),
                DeliveryPrice = new decimal(67.89)
            };

            controller.CreateProduct(productDto);

            var result = controller.DeleteProduct(productDto.Id);

            Assert.IsInstanceOfType(result, typeof(OkResult));

        }

    }

}
