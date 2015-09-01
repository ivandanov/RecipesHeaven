namespace RecipesHeaven.Services.Contracts
{
    using System.Linq;
    using System.Web.Routing;

    using RecipesHeaven.Models;
    
    public interface IUserService
    {
        User GetUser(string userId);

        IQueryable<User> GetCurrentUser(RequestContext requestContext);
    }
}
