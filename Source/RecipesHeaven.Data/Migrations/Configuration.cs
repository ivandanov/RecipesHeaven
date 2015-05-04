namespace RecipesHeaven.Data.Migrations
{
    using RecipesHeaven.Common;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<RecipesHeaven.Data.RecipesHeavenDbContext>
    {
        private const int DefaultNumberOfUsers = 10;
        private const int DefaultNumberOfProducts = 20;
        private const int DefaultNumberOfRecipse = 10;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(RecipesHeavenDbContext context)
        {
            this.SeedUsers(DefaultNumberOfUsers, context);
            this.SeedProducts(DefaultNumberOfProducts, context);
            this.SeedRecipes(DefaultNumberOfRecipse, context);
        }

        private void SeedUsers(int numberOfUsers, RecipesHeavenDbContext context)
        {
            if(context.Users.Any())
            {
                return;
            }
        }

        private void SeedRecipes(int numberOfRecipes, RecipesHeavenDbContext context)
        {
            if(context.Recipes.Any())
            {
                return;
            }
        }

        private void SeedProducts(int numberOfProducts, RecipesHeavenDbContext context)
        {
            if(context.Products.Any())
            {
                return;
            }
        }
    }
}
