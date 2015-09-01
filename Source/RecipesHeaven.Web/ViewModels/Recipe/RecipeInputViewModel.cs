namespace RecipesHeaven.Web.ViewModels.Recipe
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using RecipesHeaven.Web.Infrastructure.CustomValidation;
    using RecipesHeaven.Web.ViewModels.Product;

    public class RecipeInputViewModel : BaseViewModel
    {
        public RecipeInputViewModel()
        {
            this.Products = new List<ProductInputViewModel>();
            this.Products.Add(new ProductInputViewModel());
        }

        [Required(ErrorMessage = "This field is empty")]
        [MinLength(3, ErrorMessage = "Too short recipe name. Please write more datailed one")]
        public string Name { get; set; }

        [Required]
        [Category]
        public string Category { get; set; }

        public IList<string> PossibleCategories { get; set; }

        public IList<ProductInputViewModel> Products { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 20, ErrorMessage = "Too short preparing explanation. Please write more datailed one")]
        public string PreparingSteps { get; set; }

        //TODO:Image
    }
}