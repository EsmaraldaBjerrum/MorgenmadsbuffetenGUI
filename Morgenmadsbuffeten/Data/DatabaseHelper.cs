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

            const string receptionEmail = "reception4@hotel.com";
            const string password = "Password-123";

            if (userManager.FindByNameAsync(receptionEmail).Result == null)
            {
                var user = new IdentityUser
                {
                    UserName = receptionEmail,
                    Email = receptionEmail,
                };
                IdentityResult result = userManager.CreateAsync
                    (user, password).Result;
                if (result.Succeeded)
                {
                    var claim = new Claim("Reception", "Yes");
                    userManager.AddClaimAsync(user, claim);
                }
            }
        }
    }

}
