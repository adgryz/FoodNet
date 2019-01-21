using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FoodNET.Auth.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace FoodNET.Auth.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {

            public ApplicationDbContext CreateDbContext(string[] args)
            {

                var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

                var connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=FoodNetAuth;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=true";

                builder.UseSqlServer(connectionString);

                return new ApplicationDbContext(builder.Options);
            }
        }
    }
}
