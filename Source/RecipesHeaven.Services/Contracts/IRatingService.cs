namespace RecipesHeaven.Services.Contracts
{
    using RecipesHeaven.Models;

    public interface IRatingService
    {
        Like RateRecipe(int recipeId, string userId, int value);
    }
}
