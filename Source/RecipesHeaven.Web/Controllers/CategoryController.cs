namespace RecipesHeaven.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using RecipesHeaven.Models;
    using RecipesHeaven.Services.Contracts;
    using RecipesHeaven.Web.ViewModels.Category;

    public class CategoryController : BaseController
    {
        private ICategoryService categoryService;
        private IRecipeService recipeService;

        public CategoryController(ICategoryService categoryService, IRecipeService recipeService)
        {
            this.categoryService = categoryService;
            this.recipeService = recipeService;
        }

        public ActionResult Category(int id, int pageIndex = 0, int pageSize = 10)
        {
            var sourceCat = categoryService.GetCategoryById(id);

            var model = Mapper.Map<Category, CategoryViewModel>(sourceCat);            

            return View(model);
        }

        public ActionResult GetCategories()
        {
            var categories = categoryService.GetAllCategories()
                .AsQueryable()
                .ProjectTo<CategoryViewModel>()
                .ToList();

            return PartialView("_Categories", categories);
        }
    }
}