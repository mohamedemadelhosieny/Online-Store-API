using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Omda.APIs.Errors;
using Store.Omda.Core.Dtos.Baskets;
using Store.Omda.Core.Entities;
using Store.Omda.Core.Repositories.Contract;
using Store.Omda.Core.Services.Contract;
using Store.Omda.Service.Services.Basket;

namespace Store.Omda.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public BasketController(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserBasket>> GetBasket(string? id) 
        {
            if (id is null) return BadRequest(new ApiErrorResponse(400, "Invalid Id !!"));

            var basket = await _basketService.GetBasketAsync(id);

            if (basket is null) new UserBasket { Id = id};

            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<UserBasket>> CreateOrUpdateBasket(UserBasketDto model)
        {
            var basket = await _basketService.UpdateBasketAsync(model);

            if (basket is null) return BadRequest(new ApiErrorResponse(400));

            return Ok(basket);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string id)
        {
            if(id is null) return BadRequest(new ApiErrorResponse(400));

            var flag = await _basketService.DeleteBasketAsync(id);

            if (flag is false) return BadRequest(new ApiErrorResponse(400));

            return NoContent();
        }
    }
}
