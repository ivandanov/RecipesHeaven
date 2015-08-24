namespace RecipesHeaven.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using RecipesHeaven.Data.Contracts;
    using RecipesHeaven.Services.Contracts;
    using RecipesHeaven.Web.ViewModels.Recipe;

    public class RecipeController : BaseController
    {
        private const int DefaultNewestRecipesCount = 9;
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

        public ActionResult NewestRecipes(int count = DefaultNewestRecipesCount)
        {
            var recipes = this.recipeService.GetNewestRecipes(count)
                .AsQueryable().Project().To<RecipeOverviewModel>().ToList();

            return PartialView(recipes);
        }
    }
}