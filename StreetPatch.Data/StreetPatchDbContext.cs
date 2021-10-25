using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StreetPatch.Data.Entities;

namespace StreetPatch.Data
{
    public class StreetPatchDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public StreetPatchDbContext(DbContextOptions<StreetPatchDbContext> options)
            : base(options)
        {
            
        }
    }
}
