namespace RecipesHeaven.Web.Controllers
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;

    using RecipesHeaven.Data.Contracts;
    using RecipesHeaven.Services.Contracts;
    using RecipesHeaven.Web.ViewModels.Recipe;
    using RecipesHeaven.Models;
    using RecipesHeaven.Web.ViewModels.Comment;
    using RecipesHeaven.Web.ViewModels.Product;

    public class RecipeController : BaseController
    {
        private const int DefaultNewestRecipesCount = 9;
        private IRecipesServices recipeService;
        private ICommentsServices commentsServices;

        public RecipeController(IRecipesServices recipeService, ICommentsServices commentsServices)
        {
            this.recipeService = recipeService;
            this.commentsServices = commentsServices;
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

        [Authorize]
        public ActionResult Create()
        {
            var model = new RecipeInputViewModel();
            model.PossibleCategories = this.GetPosibleCategories();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecipeInputViewModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = this.User.Identity.GetUserId();
                    var products = model.Products.Select(m => m.Content);
                    var newRecipe = this.recipeService
                        .Create(model.Name, userId, model.Category, model.PreparingSteps, products, String.Empty);

                    return RedirectToAction("Recipe", new { id = newRecipe.Id });
                }
                catch (Exception)
                {
                    //TODO: log4net log error
                    this.ModelState.AddModelError("DataError", "There was an error while saving your recipe. Please try again later.");
                }
            }

            //dropdown list
            model.PossibleCategories = this.GetPosibleCategories();
            return View(model);
        }

        public ActionResult Add()
        {
            return PartialView("~/Views/Recipe/EditorTemplates/ProductInputViewModel.cshtml", new ProductInputViewModel());
        }

        [Authorize]
        public ActionResult MyRecipes()
        {
            var userId = this.User.Identity.GetUserId();
            var userRecipes = recipeService.GetRecipesFromUser(userId)
                .AsQueryable().Project().To<RecipeOverviewModel>().ToList();

            return View("UserRecipes", userRecipes);
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