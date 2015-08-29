namespace RecipesHeaven.Web.ViewModels.Comment
{
    using System.Collections.Generic;

    public class RecipeCommentsViewModel : BaseViewModel
    {
        public int RecipeId { get; set; }

        public string RecipeName { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}