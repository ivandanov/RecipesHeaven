namespace RecipesHeaven.Web.Controllers
{
    using System;
    using System.Net;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using RecipesHeaven.Web.ViewModels.Rating;
    using RecipesHeaven.Services.Contracts;
    using RecipesHeaven.Common;
    using RecipesHeaven.Models;
    using System.Web;

    public class RatingController : BaseController
    {
        private IRatingService ratingService;

        public RatingController(IRatingService ratingService)
        {
            this.ratingService = ratingService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LikeRecipe(RecipeRatingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userId = this.User.Identity.GetUserId();
            Like like = null;
            try
            {
                like = this.ratingService.RateRecipe(model.RecipeId, userId, model.RatedValue);
            }
            catch (RecipesHeavenException ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception)
            {
                //TODO: log4net log error
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            if (like != null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Created);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError,
                       "There is a problem on saving your rate. Please try again later.");
            }
        }
    }
}