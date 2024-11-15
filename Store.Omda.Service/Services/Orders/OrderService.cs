using Store.Omda.Core;
using Store.Omda.Core.Entities;
using Store.Omda.Core.Entities.Order;
using Store.Omda.Core.Services.Contract;
using Store.Omda.Core.Specifications.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Omda.Service.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;
        private readonly IPaymentService _paymentService;

        public OrderService(IUnitOfWork unitOfWork, IBasketService basketService, IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _basketService = basketService;
            _paymentService = paymentService;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            var basket = await _basketService.GetBasketAsync(basketId);
            if (basket is null) return null;
            
            var orderItems = new List<OrderItem>();

            if (basket.Items.Count() > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(item.Id);
                    var productOrderedItem = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
                    
                    var orderItem = new OrderItem(productOrderedItem, product.Price, item.Quantity);

                    orderItems.Add(orderItem);
                }
            }

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(deliveryMethodId);

            var subTotal = orderItems.Sum(I => I.Price * I.Quantity);


            // TODO
            if (!string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var spec = new OrderSpecificationWithPaymentIntentId(basket.PaymentIntentId);
                var ExOrder = await _unitOfWork.Repository<Order, int>().GetByIdWithSpecAsync(spec);
                _unitOfWork.Repository<Order, int>().Delete(ExOrder);
            }
            

            

             var basketDto = await _paymentService.CreateOrUpdatePaymentIntentIdAsync(basketId);

            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal, basketDto.PaymentIntentId);

            await _unitOfWork.Repository<Order,int>().AddAsync(order);
            var result= await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            return order;

        }

        public async Task<Order?> GetOrderByIdForSpacificUserAsync(string buyerEmail, int orderId)
        {
            var spec = new OrderSpecification(buyerEmail, orderId);
            var order = await _unitOfWork.Repository<Order, int>().GetByIdWithSpecAsync(spec);
            if (order is null) return null;

            return order;
        }   

        public async Task<IEnumerable<Order>?> GetOrdersForSpacificUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecification(buyerEmail);
            var orders = await _unitOfWork.Repository<Order, int>().GetAllWithSpecAsync(spec);
            if (orders is null) return null;
            return orders;
        }
    }
}
