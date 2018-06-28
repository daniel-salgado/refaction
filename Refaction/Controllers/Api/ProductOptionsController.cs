using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Refaction.Data;
using Refaction.Dtos;
using Refaction.Models;

namespace Refaction.Controllers
{

    [RoutePrefix("api/products")]
    public class ProductOptionsController : ApiController
    {

        private ApplicationDbContext _context;

        public ProductOptionsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        /// <summary>
        /// GET /products/{id}/options - finds all options for a specified product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet, Route("{productId}/options")]
        public IHttpActionResult GetProductOptions(Guid productId)
        {

            var productOptions = _context.ProductOptions.Where(p => p.ProductId.Equals(productId)).ToList();

            if (productOptions == null)
            {
                return NotFound();
            }

            return Ok(productOptions);

        }

        /// <summary>
        /// GET /products/{id}/options/{optionId} - finds the specified product option for the specified product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="optionId"></param>
        /// <returns></returns>
        [HttpGet, Route("{productId}/options/{optionId}")]
        public IHttpActionResult GetProductOption(Guid productId, Guid optionId)
        {

            var productOption = _context.ProductOptions.SingleOrDefault(p => p.ProductId.Equals(productId) && p.Id.Equals(optionId));

            if (productOption == null)
            {
                return NotFound();
            }

            return Ok(productOption);

        }

        /// <summary>
        /// POST /products/{id}/options - adds a new product option to the specified product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productOptionDto"></param>
        /// <returns></returns>
        [HttpPost, Route("{productId}/options")]
        public IHttpActionResult CreateProductOption(Guid productId, ProductOptionDto productOptionDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (_context.ProductOptions.Count(p => p.ProductId.Equals(productId) && p.Id.Equals(productOptionDto.Id)) > 0)
            {
                return Conflict();
            }

            ProductOption newproductOptionDto = new ProductOption
            {
                Id = productOptionDto.Id,
                ProductId = productId,
                Name = productOptionDto.Name,
                Description = productOptionDto.Description
            };


            try
            {
                _context.ProductOptions.Add(newproductOptionDto);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return Ok();
        }

        /// <summary>
        /// PUT /products/{id}/options/{optionId} - updates the specified product option.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="optionId"></param>
        /// <param name="productOptionDto"></param>
        /// <returns></returns>
        [HttpPut, Route("{productId}/options/{optionId}")]
        public IHttpActionResult UpdateProductOption(Guid productId, Guid optionId, ProductOptionDto productOptionDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var productOption = _context.ProductOptions.SingleOrDefault(p => p.ProductId.Equals(productId) && p.Id.Equals(optionId));

            if (productOption == null)
            {
                return NotFound();
            }

            productOption.Name = productOptionDto.Name;
            productOption.Description = productOptionDto.Description;
            productOption.ProductId = productId;

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
        /// DELETE /products/{id}/options/{optionId} - deletes the specified product option.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="optionId"></param>
        /// <returns></returns>
        [HttpDelete, Route("{productId}/options/{optionId}")]
        public IHttpActionResult DeleteProductOption(Guid productId, Guid optionId)
        {

            var productOption = _context.ProductOptions.SingleOrDefault(p => p.ProductId.Equals(productId) && p.Id.Equals(optionId));

            if (productOption == null)
            {
                return NotFound();
            }

            try
            {
                _context.ProductOptions.Remove(productOption);
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
