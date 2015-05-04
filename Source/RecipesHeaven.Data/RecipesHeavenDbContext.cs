namespace RecipesHeaven.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;

    using RecipesHeaven.Models;
    using System.Data.Entity;    

    public class RecipesHeavenDbContext : IdentityDbContext<User>
    {
        public RecipesHeavenDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Recipe> Recipes { get; set; }

        public virtual IDbSet<Product> Products { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Image> Images { get; set; }

        public static RecipesHeavenDbContext Create()
        {
            return new RecipesHeavenDbContext();
        }
    }
}
