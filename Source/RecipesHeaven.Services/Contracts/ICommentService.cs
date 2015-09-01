namespace RecipesHeaven.Services.Contracts
{
    using System.Linq;

    using RecipesHeaven.Models;

    public interface ICommentService
    {
        Comment PostComment(int recipeId, string userId, string comment);

        IQueryable<Comment> GetCommentsByRecipe(Recipe recipe, int numberOfComments);
    }
}
