using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Bll.Interfaces;
using OnlineShop.Common.Dtos.Product;

namespace OnlineShop.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class ProductController : AppBaseController
    {
        private readonly IProductServices _services;
        public ProductController(IProductServices services)
        {
            _services = services;
        }

        // api/product/{id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _services.GetProductById(id);
            return Ok(product);
        }


        // api/product/
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _services.GetAllProducts();

            return Ok(products);
        }

        // api/product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var productDto = await _services.CreateProduct(dto);

            return CreatedAtAction(nameof(GetProductById), new {Id = productDto.Id}, productDto);
        }

        // api/product
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _services.DeleteProuct(id);

            return Ok("deleted");
        }

        // api/product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var productDto = await _services.UpdateProduct(id, dto);
            return Ok(productDto);
        }
    }
}