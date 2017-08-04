namespace RecipesHeaven.Web.Controllers
{
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;

    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}