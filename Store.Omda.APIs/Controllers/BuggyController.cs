using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Omda.APIs.Errors;
using Store.Omda.Repository.Data.Contexts;

namespace Store.Omda.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]
        public async Task<IActionResult> GetNotFoundRequsetServer()
        {
            var brand =await _context.Brands.FindAsync(100);

            if (brand is null ) return NotFound(new ApiErrorResponse(404));

            return Ok(brand);

        }

        [HttpGet("servererror")]
        public async Task<IActionResult> GetServerError()
        {
            var brand = await _context.Brands.FindAsync(100);

            var brandToString = brand.ToString();

            return Ok(brand);

        }


        [HttpGet ("badrequest")]
        public async Task<IActionResult> GetBadRequestError()
        {
            return BadRequest(new ApiErrorResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public async Task<IActionResult> GetBadRequestError(int id) // validation
        {
            return Ok();
        }

        [HttpGet("unaithorized")]
        public async Task<IActionResult> GetunaithorizedError(int id)
        {
            return Unauthorized(new ApiErrorResponse(401));
        }

    }
}
