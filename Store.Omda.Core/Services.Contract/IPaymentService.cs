using Store.Omda.Core.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Omda.Core.Services.Contract
{
    public interface IPaymentService
    {
        Task<UserBasketDto> CreateOrUpdatePaymentIntentIdAsync(string basketId);

    }
}
