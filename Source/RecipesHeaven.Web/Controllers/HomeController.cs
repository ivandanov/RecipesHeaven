namespace RecipesHeaven.Web.Controllers
{
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetImage(int id)
        {
            //TODO: image process

            return null;

            //var image = this.Data.Images.GetById(id);
            //if (image == null)
            //{
            //    throw new HttpException(404, "Image not found");
            //}

            //return File(image.Content, "image/" + image.FileExtension);
        }
    }
}