namespace RecipesHeaven.Services
{
    using System.Linq;

    using RecipesHeaven.Models;
    using RecipesHeaven.Services.Contracts;
    using RecipesHeaven.Data.Contracts;

    public class CommentsServices : BaseService, ICommentsServices 
    {
        public CommentsServices(IRecipesHeavenData data)
            : base(data)
        {
        }

        public IQueryable<Comment> GetCommentsByRecipe(Recipe recipe, int numberOfComments)
        {
            return this.Data
                .Comments
                .All()
                .Where(c => c.Recipe == recipe)
                .OrderBy(c => c.CreatedOn)
                .Take(numberOfComments);
        }
    }
}
