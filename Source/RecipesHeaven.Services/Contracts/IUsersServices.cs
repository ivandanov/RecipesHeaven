namespace RecipesHeaven.Services.Contracts
{
    using System.Linq;
    using System.Web.Routing;

    using RecipesHeaven.Models;
    
    public interface IUsersServices
    {
        IQueryable<User> GetCurrentUser(RequestContext requestContext);
    }
}
