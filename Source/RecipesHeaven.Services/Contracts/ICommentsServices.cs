namespace RecipesHeaven.Services.Contracts
{
    using System.Linq;

    using RecipesHeaven.Models;

    public interface ICommentsServices
    {
        IQueryable<Comment> GetCommentsByRecipe(Recipe recipe, int numberOfComments);
    }
}
