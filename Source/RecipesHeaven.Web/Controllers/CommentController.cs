namespace RecipesHeaven.Web.Controllers
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using Microsoft.AspNet.Identity;

    using RecipesHeaven.Models;
    using RecipesHeaven.Services.Contracts;
    using RecipesHeaven.Web.ViewModels.Comment;

    public class CommentController : BaseController
    {
        private ICommentService commentService;
        private IRecipeService recipeService;

        public CommentController(ICommentService commentService, IRecipeService recipeService)
        {
            this.commentService = commentService;
            this.recipeService = recipeService;
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(CommentInputViewModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var recipe = recipeService.GetRecipeById(model.RecipeId);
                    if (recipe != null)
                    {
                        var userId = this.User.Identity.GetUserId();
                        var comment = commentService.PostComment(recipe.Id, userId, model.Comment);
                        var commentViewModel = Mapper.Map<Comment, CommentViewModel>(comment);
                        return PartialView("_SingleCommentPartial", commentViewModel);
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Recipe not found");
                    }
                }
                catch (Exception)
                {
                    //TODO: log4net log
                    ModelState.AddModelError("CommentDataError", "There was an error while saving your comment. Please try again later.");
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }

        public ActionResult GetCommentsByRecipeId()
        {
            return null;
        }

        public ActionResult DeleteComment()
        {
            return null;
        }

        public ActionResult ModifyComment()
        {
            return null;
        }
    }
}