namespace RecipesHeaven.Web.ViewModels.Product
{
using System.ComponentModel.DataAnnotations;
    public class ProductInputViewModel
    {
        [Required(ErrorMessage = "This product field is empty")]
        [StringLength(100, ErrorMessage = "Product must be at least {2} characters long.", MinimumLength = 3)]
        public string Content { get; set; }
    }
}