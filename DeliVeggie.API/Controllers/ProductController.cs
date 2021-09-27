using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Product.Domain;
using DeliVeggie.Processor;

namespace DeliVeggie.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductProcessor _productService;
        public ProductController(IProductProcessor productService)
        {
            this._productService = productService;
        }

        [HttpGet]
        [Route("Products")]
        public IActionResult GetProducts()
        {
            var response = _productService.GetProducts();
            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("ProductDetails/{id}")]
        public IActionResult GetProductDetails(int id)
        {
            var response = _productService.GetProductDetails(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}
