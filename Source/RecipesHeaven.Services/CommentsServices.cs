namespace RecipesHeaven.Services
{
    using System.Linq;

    using RecipesHeaven.Models;
    using RecipesHeaven.Services.Contracts;
    using RecipesHeaven.Data.Contracts;
    using System.Data.Entity.Core;
    using System;

    public class CommentsServices : BaseService, ICommentsServices
    {
        private IUsersServices userServices;
        private IRecipesServices recipesService;

        public CommentsServices(IRecipesHeavenData data, IUsersServices userServices, IRecipesServices recipesService)
            : base(data)
        {
            this.userServices = userServices;
            this.recipesService = recipesService;
        }

        public Comment PostComment(int recipeId, string userId, string comment)
        {
            var user = userServices.GetUser(userId);
            if (user == null)
            {
                throw new ObjectNotFoundException("User entity not found");
            }

            var recipe = recipesService.GetRecipeById(recipeId);
            if (recipe == null)
            {
                throw new ObjectNotFoundException("Recipe entity not found");
            }

            var newComment = new Comment()
            {
                AuthorId = user.Id,
                Content = comment,
                RecipeId = recipe.Id
            };

            this.Data.Comments.Add(newComment);
            this.Data.SaveChanges();

            return newComment;
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
