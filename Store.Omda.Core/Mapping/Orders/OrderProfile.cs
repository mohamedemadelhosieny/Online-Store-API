using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Omda.Core.Dtos.Auth;
using Store.Omda.Core.Dtos.Orders;
using Store.Omda.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Omda.Core.Mapping.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile( IConfiguration configuration) 
        {
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, options => options.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, options => options.MapFrom(s => s.DeliveryMethod.Cost))
                ;

            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, options => options.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, options => options.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureURL, options => options.MapFrom(s => $"{configuration["BaseUrl"]}{s.Product.PictureURL}"))
                ;
        }
    }
}
