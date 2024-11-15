using Store.Omda.Core.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Omda.Core.Services.Contract
{
    public interface IBasketService
    {
        Task<UserBasketDto?> GetBasketAsync(string basketId);
        Task<UserBasketDto?> UpdateBasketAsync(UserBasketDto basketDto);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
