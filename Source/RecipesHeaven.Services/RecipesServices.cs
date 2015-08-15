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

        public IList<Recipe> GetNewestRecipes(int numberOfRecipes = 10)
        {
            return this.Data
                .Recipes
                .All()
                .OrderByDescending(r => r.DateAdded)
                .Take(numberOfRecipes)
                .ToList();
        }

        public IList<Recipe> GetMostCommentedRecipes(int numberOfRecipes)
        {
            return this.Data
                .Recipes
                .All()
                .OrderByDescending(r => r.Comments.Count)
                .Take(numberOfRecipes)
                .ToList();
        }

        public IList<Recipe> GetMostRatedRecipes(int numberOfRecipes)
        {
            return this.Data
                .Recipes
                .All()
                .OrderByDescending(r => r.Rating.Average(a => a / r.Rating.Count))
                .Take(numberOfRecipes)
                .ToList();
        }

        public IList<Recipe> GetRecipesFromUser(IUser user)
        {
            return this.Data
                .Recipes
                .All()
                .Where(r => r.Author == user)
                .ToList();
        }

        public IList<Recipe> GetRecipesByProduct(Product product)
        {
            return this.Data
                .Recipes
                .All()
                .Where(r => r.Products.Contains(product))
                .ToList();
        }
        public IList<Recipe> GetRecipesByProducts(int[] productsIds, int pageIndex = 0, int pageSize = int.MaxValue)
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
                .OrderByDescending(r => r.Rating.Average(a => a / r.Rating.Count))
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IList<Recipe> GetRecipesByCategory(int id, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var a = this.Data
                .Recipes
                .All()
                .Where(r => r.CategoryId == id)
                .OrderByDescending(r => r.DateAdded)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();

            return a;
        }

    }
}
