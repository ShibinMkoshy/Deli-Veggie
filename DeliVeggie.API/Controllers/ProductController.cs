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
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IProductProcessor _productService;
        public ProductController(IOptions<AppSettings> settings, IProductProcessor productService)
        {
            this._appSettings = settings;
            this._productService = productService;
        }

        [HttpGet]
        [Route("Products")]
        public IActionResult GetProducts()
        {
            var response = _productService.GetProducts();
            return Ok(response);
        }

        [HttpGet]
        [Route("ProductDetails/{id}")]
        public IActionResult GetProductDetails(int id)
        {
            var response = _productService.GetProductDetails(id);
            return Ok(response);
        }
    }
}
