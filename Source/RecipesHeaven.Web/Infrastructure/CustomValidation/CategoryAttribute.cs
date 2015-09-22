namespace RecipesHeaven.Web.Infrastructure.CustomValidation
{
    using System.Linq;
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;

    using RecipesHeaven.Services.Contracts;

    public class CategoryAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueAsString = value.ToString();
            if (valueAsString == null)
            {
                return new ValidationResult("Invalid category");
            }

            //TODO: refactor hidden dependecy ICategoryService
            var categoryService = DependencyResolver.Current.GetService<ICategoryService>();
            
            var posibleCategories = categoryService.GetAllCategories().Select(c => c.Name);
            if (!posibleCategories.Contains(valueAsString))
            {
                return new ValidationResult("Category doesn't exist");
            }

            return ValidationResult.Success;
        }
    }
}