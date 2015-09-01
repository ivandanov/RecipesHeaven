namespace RecipesHeaven.Services.Contracts
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity;

    using RecipesHeaven.Models;

    public interface IRecipeService
    {
        Recipe GetRecipeById(int id);

        Recipe GetRecipeByName(string name);

        Recipe Create(string name, string authorId, string category, string preparingSteps, IEnumerable<string> products, string imgUrl);

        IList<Recipe> GetNewestRecipes(int numberOfRecipes = 10);

        IList<Recipe> GetMostCommentedRecipes(int numberOfRecipes);

        IList<Recipe> GetMostRatedRecipes(int numberOfRecipes);

        IList<Recipe> GetRecipesFromUser(string userId, int numberOfRecipes = Int32.MaxValue);

        IList<Recipe> GetRecipesByProduct(Product product);

        IList<Recipe> GetRecipesByProducts(int[] productsIds, int pageIndex, int pageSize);

        IList<Recipe> GetRecipesByCategory(int id, int pageIndex, int pageSize);
    }
}
