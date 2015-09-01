namespace RecipesHeaven.Services
{
    using System.Linq;
    using System.Web.Routing;

    using RecipesHeaven.Models;
    using RecipesHeaven.Data.Contracts;
    using RecipesHeaven.Services.Contracts;

    public class UserService : BaseService, IUserService
    {
        public UserService(IRecipesHeavenData data)
            :base(data)
        {
        }

        public User GetUser(string userId)
        {
            return this.Data.Users.GetById(userId);
        }

        public IQueryable<User> GetCurrentUser(RequestContext requestContext)
        {
            return this.Data
                .Users
                .All()
                .Where(u => u.UserName == requestContext.HttpContext.User.Identity.Name);
        }
    }
}
