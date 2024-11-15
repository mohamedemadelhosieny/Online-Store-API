using AutoMapper;
using Store.Omda.Core.Dtos.Baskets;
using Store.Omda.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Omda.Core.Mapping.Baskets
{
    public class BasketProfile :Profile
    {
        public BasketProfile()
        {
            CreateMap<UserBasket, UserBasketDto>().ReverseMap();
        }
    }
}
