using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Omda.APIs.Attributes;
using Store.Omda.Core.Services.Contract;
using Store.Omda.Core.Specifications.Products;

namespace Store.Omda.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        [Cached(100)]
        
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductspecParams productSpecParams)
        {
          var Results = await _productService.GetAllProductsAsync(productSpecParams);
            return Ok(Results);
        }


        [HttpGet("brands")]
        
        public async Task<IActionResult> GetAllBrands()
        {
            var Results = await _productService.GetAllBrandsAsync();
            return Ok(Results);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var Results = await _productService.GetAllTypesAsync();
            return Ok(Results);
        }


        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetById(int? id)
        {
            if (id == null) return BadRequest("id is not available");
            var Results = await _productService.GetProductById(id.Value);
            if(Results == null) return NotFound("product not found");
            return Ok(Results);
        }
    }
}
