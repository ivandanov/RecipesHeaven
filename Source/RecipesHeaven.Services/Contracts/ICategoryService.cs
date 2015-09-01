namespace RecipesHeaven.Services.Contracts
{
    using System.Linq;

    using RecipesHeaven.Models;
    using System.Collections.Generic;

    public interface ICategoryService
    {
        Category GetCategoryById(int id);

        Category GetCategoryByName(string name);

        IList<Category> GetAllCategories();

        IList<Category> GetMostRecipesCategory(int numberOfCategories);
    }
}
