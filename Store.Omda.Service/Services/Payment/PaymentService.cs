using Microsoft.Extensions.Configuration;
using Store.Omda.Core;
using Store.Omda.Core.Dtos.Baskets;
using Store.Omda.Core.Entities.Order;
using Store.Omda.Core.Services.Contract;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Product = Store.Omda.Core.Entities.Product;

namespace Store.Omda.Service.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public PaymentService(IBasketService basketService, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<UserBasketDto> CreateOrUpdatePaymentIntentIdAsync(string basketId)
        {

            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var basket = await _basketService.GetBasketAsync(basketId);
            if (basket is null) return null;

            var shippingPrice = 0m;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Cost;
            }

            if (basket.Items.Count() > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(item.Id);
                    if (item.Price != product.Price)
                    {
                        item.Price = product.Price;
                    }
                }
            }

            var subTotal = basket.Items.Sum(I => I.Price * I.Quantity);

            var service = new PaymentIntentService();


            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                // Create

                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)(subTotal * 100 + shippingPrice * 100),
                    PaymentMethodTypes = new List<string>() { "card" },
                    Currency = "usd"
                };

                paymentIntent = await service.CreateAsync(options);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.clientSecret = paymentIntent.ClientSecret;
            }
            else 
            {

                // Update 

                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(subTotal * 100 + shippingPrice * 100)
                };

                paymentIntent = await service.UpdateAsync( basket.PaymentIntentId ,options);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.clientSecret = paymentIntent.ClientSecret;
            }


            await _basketService.UpdateBasketAsync(basket);

            return basket;
            
        }
    }
}
