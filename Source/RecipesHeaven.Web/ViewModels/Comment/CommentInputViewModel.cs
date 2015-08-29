namespace RecipesHeaven.Web.ViewModels.Comment
{
    using System.ComponentModel.DataAnnotations;

    public class CommentInputViewModel : BaseViewModel
    {
        [Required]
        public int RecipeId { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Your comment is too long. ")]
        [MinLength(10, ErrorMessage = "Your comment is too short. ")]
        public string Comment { get; set; }
    }
}