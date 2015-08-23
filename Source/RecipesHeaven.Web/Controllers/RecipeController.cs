namespace RecipesHeaven.Web.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
    using RecipesHeaven.Data.Contracts;
    using RecipesHeaven.Services.Contracts;
    using RecipesHeaven.Web.ViewModels.Recipe;

    public class RecipeController : BaseController
    {
        private IRecipesServices recipeService;

        public RecipeController(IRecipesHeavenData data, IRecipesServices recipeService)
            : base(data)
        {
            this.recipeService = recipeService;
        }
        
        public ActionResult Recipe(int id)
        {
            var recipe = recipeService.GetRecipeById(id);

            if (recipe == null)
            {
                return HttpNotFound();
            }

            var recipeModel = Mapper.Map<RecipeViewModel>(recipe);

            return View("RecipeDetails", recipeModel);
        }
    }
}