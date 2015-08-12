﻿namespace RecipesHeaven.Services.Contracts
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity;

    using RecipesHeaven.Models;

    public interface IRecipesServices
    {
        IQueryable<Recipe> GetNewestRecipes(int numberOfRecipes = 10);

        IQueryable<Recipe> GetMostCommentedRecipes(int numberOfRecipes);

        IQueryable<Recipe> GetMostRatedRecipes(int numberOfRecipes);

        IQueryable<Recipe> GetRecipesFromUser(IUser user);

        IQueryable<Recipe> GetRecipesByProduct(Product product);

        IQueryable<Recipe> GetRecipesByProducts(int[] productsIds, int pageIndex, int pageSize);

        IQueryable<Recipe> GetRecipesByCategory(int id, int pageIndex, int pageSize);
    }
}
