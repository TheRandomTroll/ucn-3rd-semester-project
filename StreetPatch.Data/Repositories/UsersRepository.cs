using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StreetPatch.Data.Entities;

namespace StreetPatch.Data.Repositories
{
    public class UsersRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<ApplicationUser> GetByUsernameAsync(string username)
        {   
            return await this.userManager.FindByEmailAsync(username);
        }

        public string GetUsernameFromToken(IEnumerable<Claim> userClaims)
        {
            return userClaims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
        }
    }
}
