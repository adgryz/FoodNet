using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using FoodNet.ModelCore.Domain;
using Microsoft.AspNetCore.Identity;
using System;

namespace FoodNet.DataAccessCore
{
    public class FoodNetDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public FoodNetDbContext(DbContextOptions<FoodNetDbContext> options)
            : base(options)
        {
        }

        //public DbSet<User> Users { get; set; }

        public DbSet<Fridge> Fridges { get; set; }
        public DbSet<FridgeProduct> FridgeProducts { get; set; }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeProduct> RecipeProducts { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<BasicProduct> BasicProducts { get; set; }
        public DbSet<NewProduct> NewProducts { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<RecipeTag> RecipeTags { get; set; }

        public DbSet<PriorityUserProduct> PriorityUserProducts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }

    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FoodNetDbContext>
    {

        public FoodNetDbContext CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<FoodNetDbContext>();

            //var connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=FoodNet;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=true";
            var connectionString = "Server=tcp:foodnetdbserver.database.windows.net,1433;Initial Catalog=FoodNETDB;Persist Security Info=False;User ID=gryzik;Password=marker123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            builder.UseSqlServer(connectionString);

            return new FoodNetDbContext(builder.Options);
        }
    }
}
