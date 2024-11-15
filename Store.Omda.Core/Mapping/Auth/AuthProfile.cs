using AutoMapper;
using Store.Omda.Core.Dtos.Auth;
using Store.Omda.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Omda.Core.Mapping.Auth
{
    public class AuthProfile : Profile
    {
        public AuthProfile() 
        {
            CreateMap<Address,AddressDto>().ReverseMap();
        }
    }
}
