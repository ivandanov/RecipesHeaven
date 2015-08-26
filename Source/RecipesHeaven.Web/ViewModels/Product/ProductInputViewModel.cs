namespace RecipesHeaven.Web.ViewModels.Product
{
using System.ComponentModel.DataAnnotations;
    public class ProductInputViewModel
    {
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Content { get; set; }
    }
}