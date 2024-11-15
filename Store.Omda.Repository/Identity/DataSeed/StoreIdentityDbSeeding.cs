using Microsoft.AspNetCore.Identity;
using Store.Omda.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Omda.Repository.Identity.DataSeed
{
    public class StoreIdentityDbSeeding
    {
        public async static Task SeedAppUserAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count()==0)
            {
                var user = new AppUser()
                {
                    Email = "me38310@gmail.com",
                    DisplayName = "omda",
                    UserName = "Muhamed.emad",
                    PhoneNumber = "01068548607",
                    Address = new Address()
                    {
                        FName = "Muhamed",
                        LName = "Emad",
                        City = "Mansoura",
                        Street ="Nakhla",
                        Country = "Egypt"
                    }
                };

                await _userManager.CreateAsync(user, "Meme+552001");
            }
        }
    }
}
