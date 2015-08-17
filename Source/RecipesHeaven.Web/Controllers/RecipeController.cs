using RecipesHeaven.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipesHeaven.Web.Controllers
{
    public class RecipeController : BaseController
    {
        public RecipeController(IRecipesHeavenData data)
            : base(data)
        {

        }
        // GET: Recipe
        public ActionResult Recipe(int id)
        {
            return View();
        }
    }
}