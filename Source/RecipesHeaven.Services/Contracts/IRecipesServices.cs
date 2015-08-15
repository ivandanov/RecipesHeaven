namespace RecipesHeaven.Services.Contracts
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity;

    using RecipesHeaven.Models;

    public interface IRecipesServices
    {
        IList<Recipe> GetNewestRecipes(int numberOfRecipes = 10);

        IList<Recipe> GetMostCommentedRecipes(int numberOfRecipes);

        IList<Recipe> GetMostRatedRecipes(int numberOfRecipes);

        IList<Recipe> GetRecipesFromUser(IUser user);

        IList<Recipe> GetRecipesByProduct(Product product);

        IList<Recipe> GetRecipesByProducts(int[] productsIds, int pageIndex, int pageSize);

        IList<Recipe> GetRecipesByCategory(int id, int pageIndex, int pageSize);
    }
}
