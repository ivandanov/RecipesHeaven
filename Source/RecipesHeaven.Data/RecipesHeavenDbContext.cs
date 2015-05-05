namespace RecipesHeaven.Data
{
    using System;
    using System.Linq;
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using RecipesHeaven.Models;
    using RecipesHeaven.Models.Contracts;

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

        public override int SaveChanges()
        {
            this.ApplyCreateableEntitiesRules();
            return base.SaveChanges();
        }

        private void ApplyCreateableEntitiesRules()
        {
            var entities = this.ChangeTracker
                .Entries()
                .Where(e => e.Entity is ICreatableEntity
                        && ((e.State == EntityState.Added) 
                            || (e.State == EntityState.Modified)));

            foreach (var entry in entities)
            {
                var entity = (ICreatableEntity) entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
