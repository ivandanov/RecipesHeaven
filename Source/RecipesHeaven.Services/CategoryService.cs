﻿namespace RecipesHeaven.Services
{
    using System.Linq;

    using RecipesHeaven.Models;
    using RecipesHeaven.Data.Contracts;
    using RecipesHeaven.Services.Contracts;
    using System.Collections.Generic;

    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(IRecipesHeavenData data)
            :base(data)
        {
        }

        public Category GetCategoryById(int id)
        {
            return this.Data.Categories.GetById(id);
        }

        public Category GetCategoryByName(string name)
        {
            return this.Data.Categories.All().FirstOrDefault(c => c.Name == name);
        }

        public IList<Category> GetAllCategories()
        {
            return this.Data
                .Categories
                .All()
                .ToList();
        }

        public IList<Category> GetCategoryByRecipe(Recipe recipe)
        {
            return this.Data
                .Categories
                .All()
                .Where(c => c.Recipes.Contains(recipe))
                .ToList();
        }

        public IList<Category> GetMostRecipesCategory(int numberOfCategories)
        {
            return this.Data
                .Categories
                .All()
                .OrderByDescending(c => c.Recipes.Count)
                .ToList();
        }
    }
}
