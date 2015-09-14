namespace RecipesHeaven.Services
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Entity.Core;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity;

    using RecipesHeaven.Models;
    using RecipesHeaven.Data.Contracts;
    using RecipesHeaven.Services.Contracts;

    public class RecipeService : BaseService, IRecipeService
    {
        private ICategoryService categoryServices;

        public RecipeService(IRecipesHeavenData data, ICategoryService categoryServices)
            : base(data)
        {
            this.categoryServices = categoryServices;
        }
        
        public Recipe GetRecipeById(int id)
        {
            return this.Data.Recipes.GetById(id);
        }

        public Recipe GetRecipeByName(string name)
        {
            return this.Data.Recipes.All().FirstOrDefault(r => r.Name == name);
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

        public Recipe Create(string name, string authorId, string category, string preparingSteps, IEnumerable<string> products, string imgUrl)
        {
            var categoryEntity = categoryServices.GetCategoryByName(category);
            if(categoryEntity == null)
            {
                throw new ObjectNotFoundException(string.Format("\"{0}\" category doesn't exist", category));
            }

            var isNameFree = this.GetRecipeByName(name) == null;
            if(!isNameFree)
            {
                throw new InvalidOperationException(string.Format("There is already a recipe with name: {0}", name));
            }

            var productsEntity = products
                .Select(pr => new Product() { Content = pr })
                .ToList();

            var newRecipe = new Recipe()
            {
                Name = name,
                AuthorId = authorId,
                Category = categoryEntity,
                PreparingSteps = preparingSteps,
                Products = productsEntity,
                ImageUrl = imgUrl,
                DateAdded = DateTime.Now
            };

            this.Data.Recipes.Add(newRecipe);
            this.Data.SaveChanges();

            return newRecipe;
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
                .OrderByDescending(r => r.Rating.Average(a => a.Value))
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
                .OrderByDescending(r => r.Rating.Average(a => a.Value))
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

        public IList<Recipe> GetRecipesFromUser(string userId, int numberOfRecipes = Int32.MaxValue)
        {
            var recipes = this.Data.Recipes
                .All()
                .Where(r => r.Author.Id == userId)
                .OrderByDescending(r => r.DateAdded)
                .Take(numberOfRecipes)
                .ToList();

            return recipes;
        }
    }
}
