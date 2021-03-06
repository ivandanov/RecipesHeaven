﻿namespace RecipesHeaven.Web.Controllers
{
    using System; 
    using System.Web;
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
     
    using RecipesHeaven.Services.Contracts;
    using RecipesHeaven.Web.ViewModels.Recipe;
    using RecipesHeaven.Models; 
    using RecipesHeaven.Web.Infrastructure.ImageProcessing;
    using RecipesHeaven.Common; 

    public class RecipeController : BaseController
    {
        private const int DefaultNewestRecipesCount = 9;
        private IRecipeService recipeService;
        private ICommentService commentsServices;
        private ICategoryService categoryService;

        public RecipeController(IRecipeService recipeService, ICommentService commentsServices, ICategoryService categoryService)
        {
            this.recipeService = recipeService;
            this.commentsServices = commentsServices;
            this.categoryService = categoryService;
        }
        public ActionResult Recipe(int id, bool isCreated = false)
        {
            var recipe = recipeService.GetRecipeById(id);

            if (recipe == null)
            {
                return HttpNotFound();
            }

            var recipeModel = Mapper.Map<RecipeViewModel>(recipe);

            var userId = this.User.Identity.GetUserId();
            recipeModel.IsCurrentUserRatedThis = recipe.Rating.Any(l => l.UserId == userId);

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
                return this.Create();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isRecipeNameFree = this.recipeService.GetRecipeByName(model.Name) == null;
            if (!isRecipeNameFree)
            {
                this.ModelState.AddModelError("DataError", "There is already a recipe with this name. Please check it before post your recipe");
                return View(model);
            }

            if (Request.Files.Count != 1)
            {
                this.ModelState.AddModelError("DataError", "You should select one image for the recipe");
                return View(model);
            }

            var imageName = this.SaveImage(Request.Files[0]);
            if (imageName == null)
            {
                this.ModelState.AddModelError("DataError", "There is a problem with your recipe image. Please check it once again");
                return View(model);
            }

            var userId = this.User.Identity.GetUserId();
            var products = model.Products.Select(m => m.Content);
            Recipe newRecipe = null;
            try
            {
                newRecipe = this.recipeService
                    .Create(model.Name, userId, model.Category, model.PreparingSteps, products, imageName);
            }
            catch (RecipesHeavenException ex)
            {
                this.ModelState.AddModelError("DataError", ex.Message);
                return View(model);
            }
            catch (Exception)
            {
                //TODO: log4net log error
                this.ModelState.AddModelError("DataError",
                    "There was an error while saving your recipe. Please try again later.");
                return View(model);
            }

            return RedirectToAction("Recipe", new { id = newRecipe.Id, isCreated = true });
        }


        [Authorize]
        public ActionResult MyRecipes()
        {
            var userId = this.User.Identity.GetUserId();
            var userRecipes = recipeService.GetRecipesFromUser(userId)
                .AsQueryable().ProjectTo<RecipeViewModel>().ToList();

            return View("UserRecipes", userRecipes);
        }

        [ChildActionOnly]
        public ActionResult NewestRecipes(int count = DefaultNewestRecipesCount)
        {
            var recipes = this.recipeService.GetNewestRecipes(count)
                .AsQueryable().ProjectTo<RecipeOverviewModel>().ToList();

            return PartialView("_NewestRecipes", recipes);
        }

        [NonAction]
        public IList<string> GetPosibleCategories()
        {
            var posibleCategories = this.categoryService
                .GetAllCategories()
                .Select(c => c.Name)
                .ToList();

            return posibleCategories;
        }

        [NonAction]
        private string SaveImage(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                ImageUpload imageUpload = new ImageUpload { Height = ImageConfiguration.Height };
                ImageResult imageResult = imageUpload.RenameUploadFile(file);

                if (imageResult.Success)
                {
                    return imageResult.ImageName;
                }
            }

            return null;
        }
    }
}