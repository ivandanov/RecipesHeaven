using System.Linq;
using System.Web;

namespace RecipesHeaven.Web.Controllers
{
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using RecipesHeaven.Data.Contracts;
    using RecipesHeaven.Services.Contracts;
    using RecipesHeaven.Web.ViewModels.Home;
    using RecipesHeaven.Web.ViewModels.Recipe;

    public class HomeController : BaseController
    {
        private readonly IRecipesServices recipeServices;

        public HomeController(IRecipesHeavenData data, IRecipesServices recipeServices)
            : base(data)
        {
            this.recipeServices = recipeServices;
        }

        public ActionResult Index()
        {
            var model = new IndexViewModel();

            model.NewestRecipes = recipeServices
                .GetNewestRecipes()
                .AsQueryable()
                .Project()
                .To<RecipeOverviewModel>()
                .ToList();

            return View(model);
        }

        public ActionResult GetImage(int id)
        {
            var image = this.Data.Images.GetById(id);
            if (image == null)
            {
                throw new HttpException(404, "Image not found");
            }

            return File(image.Content, "image/" + image.FileExtension);
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}