using Microsoft.AspNetCore.Identity;
using Store.Omda.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Omda.Core.Services.Contract
{
    public interface ITokenService
    {
        Task<string> CreateTokenAysnc(AppUser user, UserManager<AppUser> userManager);
    }
}
