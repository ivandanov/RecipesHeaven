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
    using System.Data.Entity.Core;

    public class RecipesServices : BaseService, IRecipesServices
    {
        private ICategoriesServices categoryServices;

        public RecipesServices(IRecipesHeavenData data, ICategoriesServices categoryServices)
            : base(data)
        {
            this.categoryServices = categoryServices;
        }

        public Recipe GetRecipeById(int id)
        {
            return this.Data.Recipes.GetById(id);
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

        public Recipe Create(string name, User author, string category, string preparingSteps, string imgUrl)
        {
            var categoryEntity = categoryServices.GetCategoryByName(category);
            if(categoryEntity == null)
            {
                throw new ObjectNotFoundException(string.Format("\"{0}\" category doesn't exist", category));
            }

            //var recipe = new Recipe()
            //{
            //    Name = random.RandomString(10, 49),
            //    Author = someUsers[random.Next(0, someUsers.Count)],
            //    Category = someCategories[random.Next(0, someCategories.Count)],
            //    PreparingSteps = random.RandomString(20, 500),
            //    Image = this.GetSampleImage(DefaultRecipeImagesPath),
            //    DateAdded = DateTime.Now
            //};

            return null;
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

            var query = from recipes in this.Data.Recipes.All()
                        where productsIds.All(id => 
                            recipes.Products.Select(pr => pr.Id).Contains(id))
                        select recipes;

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
