using StackExchange.Redis;
using Store.Omda.Core.Entities;
using Store.Omda.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Omda.Repository.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }


        public async Task<UserBasket?> GetBasketAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);

            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<UserBasket>(basket);
        }

        public async Task<UserBasket?> UpdateBasketAsync(UserBasket basket)
        {
            var createdOrDeletedBasket = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket),
                                                                        TimeSpan.FromDays(30));

            if (createdOrDeletedBasket is false) return null;

            return await GetBasketAsync(basket.Id);
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

    }
}
