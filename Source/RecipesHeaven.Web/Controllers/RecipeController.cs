namespace RecipesHeaven.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using RecipesHeaven.Data.Contracts;
    using RecipesHeaven.Services.Contracts;
    using RecipesHeaven.Web.ViewModels.Recipe;
    using System.Net;
    using System.Collections.Generic;

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

        public ActionResult Create()
        {
            var model = new NewRecipeViewModel();
            model.PossibleCategories = this.GetPosibleCategories();
            
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewRecipeViewModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!ModelState.IsValid)
            {
                //categories for dropdown list
                model.PossibleCategories = GetPosibleCategories();
                return View(model);
            }



            return RedirectToAction("Recipe", new { id = 1 });
        }


        public ActionResult MyRecipes()
        {
            return HttpNotFound();
        }

        [ChildActionOnly]
        public ActionResult NewestRecipes(int count = DefaultNewestRecipesCount)
        {
            var recipes = this.recipeService.GetNewestRecipes(count)
                .AsQueryable().Project().To<RecipeOverviewModel>().ToList();

            return PartialView("_NewestRecipes", recipes);
        }

        [NonAction]
        public IEnumerable<string> GetPosibleCategories()
        {
            var categoryService = DependencyResolver.Current.GetService<ICategoriesServices>();
            var posibleCategories = categoryService.GetAllCategories().Select(c => c.Name);

            return posibleCategories;
        }
    }
}