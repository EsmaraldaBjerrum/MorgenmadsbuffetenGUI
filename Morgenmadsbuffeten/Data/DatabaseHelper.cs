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

        public void CreateReceptionUser(UserManager<IdentityUser> userManager)
        {

            const string receptionEmail = "reception@hotel.com";
            const string password = "password123";

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
                    var adminClaim = new Claim("Admin", "Yes");
                    userManager.AddClaimAsync(user, adminClaim);
                }
            }
        }
    }

}
