using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Refaction.Data;
using Refaction.Dtos;
using Refaction.Models;
using System.Web.Http.Description;

namespace Refaction.Controllers
{

    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {

        private ApplicationDbContext _context;

        public ProductsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        /// <summary>
        /// GET /products - gets all products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetProducts()
        {

            var products = _context.Products.ToList();

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);

        }

        /// <summary>
        /// GET /products?name={name} - finds all products matching the specified name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet, ResponseType(typeof(ProductDto))]
        public IHttpActionResult GetProducts(string name)
        {

            var products = _context.Products.Where(c => c.Name.Contains(name)).ToList();

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);

        }

        /// <summary>
        /// GET /products/{id} - gets the project that matches the specified ID - ID is a GUID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetProduct(Guid id)
        {

            var product = _context.Products.Where(p => p.Id.Equals(id)).ToList();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);

        }

        /// <summary>
        /// POST /products - creates a new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateProduct(ProductDto product)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (_context.Products.Count(p => p.Id.Equals(product.Id)) > 0)
            {
                return Conflict();
            }


            Product newProduct = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DeliveryPrice = product.DeliveryPrice
            };


            try
            {
                _context.Products.Add(newProduct);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return Ok();
        }

        /// <summary>
        /// PUT /products/{id} - updates a product
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPut, Route("{id}")]
        public IHttpActionResult UpdateProduct(Guid id, ProductDto productDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var product = _context.Products.SingleOrDefault(c => c.Id.Equals(id));

            if (product == null)
            {
                return NotFound();
            }


            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.DeliveryPrice = productDto.DeliveryPrice;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Ok();

        }

        /// <summary>
        /// DELETE /products/{id} - deletes a product and its options.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Route("{id}")]
        public IHttpActionResult DeleteProduct(Guid id)
        {

            var product = _context.Products.SingleOrDefault(p => p.Id.Equals(id));

            if (product == null)
            {
                return NotFound();
            }

            try
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();

        }

    }

}
