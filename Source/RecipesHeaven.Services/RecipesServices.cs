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

        public IQueryable<Recipe> GetNewestRecipes(int numberOfRecipes = 10)
        {
            return this.Data
                .Recipes
                .All()
                .OrderByDescending(r => r.DateAdded)
                .Take(numberOfRecipes);
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
        public IQueryable<Recipe> GetRecipesByProducts(int[] productsIds, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            //check certain recipe contains all given products
            //slower aproach:
            //Expression<Func<Recipe, bool>> containsAllProductsInRecipe2 =
            //    recipe => productsIds.All(p => recipe.Products.Select(pr => pr.Id).Contains(p));

            Expression<Func<Recipe, bool>> containsAllProductsInRecipe =
                recipe => recipe.Products
                    .Select(pr => pr.Id)
                    .Intersect(productsIds)
                    .Count() == productsIds.Length;

            return this.Data
                .Recipes
                .All()
                .Where(containsAllProductsInRecipe)
                .Skip(pageIndex * pageSize)
                .Take(pageSize);
        }

        public IQueryable<Recipe> GetRecipesByCategory(int id, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return this.Data
                .Recipes
                .All()
                .Where(r => r.Category.Id == id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize);
        }

    }
}
