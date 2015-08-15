namespace RecipesHeaven.Services.Contracts
{
    using System.Linq;

    using RecipesHeaven.Models;
    using System.Collections.Generic;

    public interface ICategoriesServices
    {
        Category GetCategoryById(int id);

        IList<Category> GetAllCategories();

        IList<Category> GetCategoryByRecipe(Recipe recipe);

        IList<Category> GetMostRecipesCategory(int numberOfCategories);
    }
}
