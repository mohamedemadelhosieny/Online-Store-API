using Store.Omda.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Omda.Core.Services.Contract
{
    public interface IOrderService
    {
       Task<Order> CreateOrderAsync(string buyerEmail,string basketId, int deliveryMethodId, Address shippingAddress);

       Task<IEnumerable<Order>?> GetOrdersForSpacificUserAsync(string buyerEmail);
       Task<Order?> GetOrderByIdForSpacificUserAsync(string buyerEmail, int orderId);
    }
}
