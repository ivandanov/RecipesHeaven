namespace RecipesHeaven.Web.ViewModels.Recipe
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using RecipesHeaven.Web.Infrastructure.CustomValidation;
    using RecipesHeaven.Web.ViewModels.Product;

    public class NewRecipeViewModel : BaseViewModel
    {
        public IEnumerable<string> PossibleCategories { get; set; }

        public NewRecipeViewModel()
        {
            this.Products = new List<ProductInputViewModel>();
            this.Products.Add(new ProductInputViewModel());
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [Category]
        public string Category { get; set; }

        public IList<ProductInputViewModel> Products { get; set; }

        public string PreparingSteps { get; set; }

        //TODO:Image
    }
}