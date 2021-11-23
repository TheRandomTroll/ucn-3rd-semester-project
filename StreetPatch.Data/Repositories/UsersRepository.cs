using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
