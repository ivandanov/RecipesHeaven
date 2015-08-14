using RecipesHeaven.Data.Contracts;
using RecipesHeaven.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using RecipesHeaven.Web.ViewModels.Category;
using RecipesHeaven.Web.ViewModels.Recipe;

namespace RecipesHeaven.Web.Controllers
{
    public class CategoryController : BaseController
    {
        private ICategoriesServices categoryService;
        private IRecipesServices recipeService;


        public CategoryController(IRecipesHeavenData data, ICategoriesServices categoryService, IRecipesServices recipeService)
           : base(data)
        {
            this.categoryService = categoryService;
            this.recipeService = recipeService;
        }

        // GET: Category
        public ActionResult CategoryRecipes(int id, int pageIndex = 0, int pageSize = 10)
        {
            var recipesInCategory = recipeService.GetRecipesByCategory(id, pageIndex, pageSize)
                .Project().To<RecipeOverviewModel>().ToList();

            return View(recipesInCategory);
        }

        public ActionResult GetCategories()
        {
            var categories = categoryService.GetAllCategories()
                .Project().To<CategoryViewModel>().ToList();

            return PartialView("_Categories", categories);
        }
    }
}