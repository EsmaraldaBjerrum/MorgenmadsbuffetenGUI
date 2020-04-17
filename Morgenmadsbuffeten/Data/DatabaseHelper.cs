using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Morgenmadsbuffeten.Data
{
    public class DatabaseHelper
    {
        public void CreateUsers(UserManager<IdentityUser> userManager)
        {
            CreateReceptionUser(userManager);
            CreateRestaurantUser(userManager);
        }
        private async void CreateReceptionUser(UserManager<IdentityUser> userManager)
        {

            const string receptionEmail = "Reception@OurHotel.com";
            const string password = "Password-123";

            if (userManager.FindByNameAsync(receptionEmail).Result == null)
            {
                var user = new IdentityUser
                {
                    UserName = receptionEmail,
                    Email = receptionEmail
      
                };
                IdentityResult result = userManager.CreateAsync
                    (user, password).Result;
                if (result.Succeeded)
                {
                    var claim = new Claim("Reception", "Yes");
                    userManager.AddClaimAsync(user, claim).Wait();
                    string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    userManager.ConfirmEmailAsync(user, token).Wait();

                }
            }
        }

        public async void CreateRestaurantUser(UserManager<IdentityUser> userManager)
        {
            const string restaurantEmail = "Restaurant@OurHotel.com";
            const string password = "Password-123";

            if (userManager.FindByNameAsync(restaurantEmail).Result == null)
            {
                var user = new IdentityUser
                {
                    UserName = restaurantEmail,
                    Email = restaurantEmail

                };
                IdentityResult result = userManager.CreateAsync
                    (user, password).Result;
                if (result.Succeeded)
                {
                    var claim = new Claim("Restaurant", "Yes");
                    userManager.AddClaimAsync(user, claim).Wait();
                    string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    userManager.ConfirmEmailAsync(user, token).Wait();
                }
            }
        }
    }

}
