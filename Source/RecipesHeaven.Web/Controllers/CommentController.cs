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

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(CommentInputViewModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No data received");
            }

            if (ModelState.IsValid)
            {
                var userId = this.User.Identity.GetUserId();
                try
                {
                    var recipe = recipeService.GetRecipeById(model.RecipeId);
                    if (recipe != null)
                    {
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
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, 
                        "There was an error while saving your comment. Please try again later.");
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Your comment has bad format");
        }

        public ActionResult GetCommentsByRecipeId()
        {
            throw new NotImplementedException();
        }

        public ActionResult DeleteComment()
        {
            throw new NotImplementedException();
        }

        public ActionResult ModifyComment()
        {
            throw new NotImplementedException();
        }
    }
}