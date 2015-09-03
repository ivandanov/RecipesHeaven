namespace RecipesHeaven.Web.ViewModels.Comment
{
    using System.ComponentModel.DataAnnotations;

    public class CommentInputViewModel : BaseViewModel
    {
        [Required]
        public int RecipeId { get; set; }

        [MaxLength(1000, ErrorMessage = "Your comment is too long. ")]
        [MinLength(3, ErrorMessage = "Your comment is too short. ")]
        public string Comment { get; set; }
    }
}