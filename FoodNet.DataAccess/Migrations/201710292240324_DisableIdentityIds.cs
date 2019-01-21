namespace FoodNet.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisableIdentityIds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FridgeProducts", "FridgeId", "dbo.Fridges");
            DropForeignKey("dbo.Fridges", "UserId", "dbo.Users");
            DropForeignKey("dbo.Recipes", "UserId", "dbo.Users");
            DropForeignKey("dbo.FridgeProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.RecipeProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.RecipeProducts", "RecipeId", "dbo.Recipes");
            DropPrimaryKey("dbo.FridgeProducts");
            DropPrimaryKey("dbo.Fridges");
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.Products");
            DropPrimaryKey("dbo.ProductCategories");
            DropPrimaryKey("dbo.RecipeProducts");
            DropPrimaryKey("dbo.Recipes");
            AlterColumn("dbo.FridgeProducts", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Fridges", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Users", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Products", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.ProductCategories", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.RecipeProducts", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Recipes", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.FridgeProducts", "Id");
            AddPrimaryKey("dbo.Fridges", "Id");
            AddPrimaryKey("dbo.Users", "Id");
            AddPrimaryKey("dbo.Products", "Id");
            AddPrimaryKey("dbo.ProductCategories", "Id");
            AddPrimaryKey("dbo.RecipeProducts", "Id");
            AddPrimaryKey("dbo.Recipes", "Id");
            AddForeignKey("dbo.FridgeProducts", "FridgeId", "dbo.Fridges", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Fridges", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Recipes", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FridgeProducts", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RecipeProducts", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RecipeProducts", "RecipeId", "dbo.Recipes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecipeProducts", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.RecipeProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.FridgeProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Recipes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Fridges", "UserId", "dbo.Users");
            DropForeignKey("dbo.FridgeProducts", "FridgeId", "dbo.Fridges");
            DropPrimaryKey("dbo.Recipes");
            DropPrimaryKey("dbo.RecipeProducts");
            DropPrimaryKey("dbo.ProductCategories");
            DropPrimaryKey("dbo.Products");
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.Fridges");
            DropPrimaryKey("dbo.FridgeProducts");
            AlterColumn("dbo.Recipes", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.RecipeProducts", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.ProductCategories", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Products", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Users", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Fridges", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.FridgeProducts", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Recipes", "Id");
            AddPrimaryKey("dbo.RecipeProducts", "Id");
            AddPrimaryKey("dbo.ProductCategories", "Id");
            AddPrimaryKey("dbo.Products", "Id");
            AddPrimaryKey("dbo.Users", "Id");
            AddPrimaryKey("dbo.Fridges", "Id");
            AddPrimaryKey("dbo.FridgeProducts", "Id");
            AddForeignKey("dbo.RecipeProducts", "RecipeId", "dbo.Recipes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RecipeProducts", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FridgeProducts", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Recipes", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Fridges", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FridgeProducts", "FridgeId", "dbo.Fridges", "Id", cascadeDelete: true);
        }
    }
}
