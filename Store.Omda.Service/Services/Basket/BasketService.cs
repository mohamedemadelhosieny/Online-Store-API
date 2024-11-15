using AutoMapper;
using Store.Omda.Core.Dtos.Baskets;
using Store.Omda.Core.Entities;
using Store.Omda.Core.Repositories.Contract;
using Store.Omda.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Omda.Service.Services.Basket
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<UserBasketDto?> GetBasketAsync(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);

            if (basket is null) return _mapper.Map<UserBasketDto>(new UserBasket() { Id = basketId});

            return _mapper.Map<UserBasketDto>(basket);
        }

        public async Task<UserBasketDto?> UpdateBasketAsync(UserBasketDto basketDto)
        {
            var basket = await _basketRepository.UpdateBasketAsync(_mapper.Map<UserBasket>(basketDto));

            if (basket is null) return null;

            return _mapper.Map<UserBasketDto>(basket);
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _basketRepository.DeleteBasketAsync(basketId);
        }
    }
}
