namespace RecipesHeaven.Services
{
    using System.Linq;

    using RecipesHeaven.Models;
    using RecipesHeaven.Data.Contracts;
    using RecipesHeaven.Services.Contracts;

    public class CategoriesServices : BaseService, ICategoriesServices
    {
        public CategoriesServices(IRecipesHeavenData data)
            :base(data)
        {
        }

        public IQueryable<Category> GetCategoryByRecipe(Recipe recipe)
        {
            return this.Data
                .Categories
                .All()
                .Where(c => c.Recipes.Contains(recipe));
        }

        public IQueryable<Category> GetMostRecipesCategory(int numberOfCategories)
        {
            return this.Data
                .Categories
                .All()
                .OrderByDescending(c => c.Recipes.Count);
        }
    }
}
