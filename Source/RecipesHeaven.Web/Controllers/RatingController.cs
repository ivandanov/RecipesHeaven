﻿namespace RecipesHeaven.Web.Controllers
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

    public class RatingController : Controller
    {
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LikeRecipe(RecipeRatingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ratingService = DependencyResolver.Current.GetService<IRatingService>();
            var userId = this.User.Identity.GetUserId();
            Like like = null;
            try
            {
                like = ratingService.RateRecipe(model.RecipeId, userId, model.RatedValue);
            }
            catch (RecipesHeavenException ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception)
            {
                //TODO: log4net log error
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