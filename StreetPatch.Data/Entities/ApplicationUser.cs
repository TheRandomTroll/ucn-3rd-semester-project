using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StreetPatch.Data.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }

    }
}
