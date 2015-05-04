using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RecipesHeaven.Web.Startup))]
namespace RecipesHeaven.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
