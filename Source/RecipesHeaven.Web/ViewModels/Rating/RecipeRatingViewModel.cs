namespace RecipesHeaven.Web.ViewModels.Rating
{
    using System.ComponentModel.DataAnnotations;

    public class RecipeRatingViewModel
    {
        [Required]
        public int RecipeId { get; set; }

        [Required]
        [Range(1, 5)]
        public int RatedValue { get; set; }
    }
}