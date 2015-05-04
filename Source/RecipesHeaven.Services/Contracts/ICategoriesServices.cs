namespace RecipesHeaven.Services.Contracts
{
    using System.Linq;

    using RecipesHeaven.Models;

    public interface ICategoriesServices
    {
        IQueryable<Category> GetCategoryByRecipe(Recipe recipe);

        IQueryable<Category> GetMostRecipesCategory(int numberOfCategories);
    }
}
