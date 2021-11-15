using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StreetPatch.Data.Entities;
using StreetPatch.Data.Entities.Base;

namespace StreetPatch.Data
{
    public class StreetPatchDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public StreetPatchDbContext(DbContextOptions<StreetPatchDbContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Report>()
                .OwnsOne(typeof(Coordinates), "Coordinates");
            builder.Entity<Report>()
                .Property(c => c.Type)
                .HasConversion<string>();

            builder.Entity<Report>()
                .Property(c => c.Status)
                .HasConversion<string>();

            base.OnModelCreating(builder);
        }
    }
}
