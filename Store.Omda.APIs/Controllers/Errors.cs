using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Omda.APIs.Errors;

namespace Store.Omda.APIs.Controllers
{
    [Route("error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class Errors : ControllerBase
    {

        public IActionResult Error(int code)
        {
            return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "End Point not found"));
        }
    }
}
