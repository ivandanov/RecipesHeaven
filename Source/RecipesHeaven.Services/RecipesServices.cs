namespace RecipesHeaven.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity;

    using RecipesHeaven.Data.Contracts;
    using RecipesHeaven.Services.Contracts;
    using RecipesHeaven.Models;
    using System.Linq.Expressions;
    using System;

    public class RecipesServices : BaseService, IRecipesServices
    {
        public RecipesServices(IRecipesHeavenData data)
            : base(data)
        {
        }

        public IQueryable<Recipe> GetMostCommentedRecipes(int numberOfRecipes)
        {
            return this.Data
                .Recipes
                .All()
                .OrderByDescending(r => r.Comments.Count)
                .Take(numberOfRecipes);
        }

        public IQueryable<Recipe> GetMostRatedRecipes(int numberOfRecipes)
        {
            return this.Data
                .Recipes
                .All()
                .OrderByDescending(r => r.Rating.Average(a => a / r.Rating.Count))
                .Take(numberOfRecipes);
        }

        public IQueryable<Recipe> GetRecipesFromUser(IUser user)
        {
            return this.Data
                .Recipes
                .All()
                .Where(r => r.Author == user);
        }
        
        public IQueryable<Recipe> GetRecipesByProduct(Product product)
        {
            return this.Data
                .Recipes
                .All()
                .Where(r => r.Products.Contains(product));
        }
        public IQueryable<Recipe> GetRecipesByProducts(IEnumerable<Product> products)
        {
            //check certain recipe contains all given products
            Expression<Func<Recipe, bool>> containsAllProductsInRecipe = 
                r => r.Products.Intersect(r.Products).Count() == products.Count();

            //other approach (slower)
            //Expression<Func<Recipe, bool>> containsAllProductsInRecipe =
            //    r => products.All(p => r.Products.Contains(p));

            return this.Data
                .Recipes
                .All()
                .Where(containsAllProductsInRecipe);
        }

        public IQueryable<Recipe> GetRecipesByCategory(Category category)
        {
            return this.Data
                .Recipes
                .All()
                .Where(r => r.Category == category);
        }
    }
}
