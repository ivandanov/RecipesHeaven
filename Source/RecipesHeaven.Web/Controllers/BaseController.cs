namespace RecipesHeaven.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Routing;
    using System.Web.Mvc;

    using RecipesHeaven.Data.Contracts;
    using RecipesHeaven.Models;
    using RecipesHeaven.Services.Contracts;

    public class BaseController : Controller
    {
        public BaseController(IRecipesHeavenData data)
        {
            this.Data = data;
        }

        protected IRecipesHeavenData Data { get; private set; }

        protected User CurrentUser { get; set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            var service = (IUsersServices) DependencyResolver.Current.GetService(typeof(IUsersServices));

            this.CurrentUser = service
                    .GetCurrentUser(requestContext)
                    .FirstOrDefault();

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}