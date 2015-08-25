namespace RecipesHeaven.Web.ViewModels.Recipe
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using RecipesHeaven.Web.Infrastructure.CustomValidation;

    public class NewRecipeViewModel : BaseViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Category]
        public string Category { get; set; }

        public IEnumerable<string> Products { get; set; }

        public string PreparingSteps { get; set; }

        //TODO:Image
    }
}