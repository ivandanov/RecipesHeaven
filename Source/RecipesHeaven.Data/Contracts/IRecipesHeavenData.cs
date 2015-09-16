namespace RecipesHeaven.Data.Contracts
{
    using System;
    using System.Data.Entity;

    using RecipesHeaven.Models;

    public interface IRecipesHeavenData
    {
        DbContext Context { get; }

        IRepository<Category> Categories { get; }

        IRepository<Comment> Comments { get; }
        
        IRepository<Recipe> Recipes { get; }

        IRepository<Product> Products { get; }

        IRepository<User> Users { get; }

        void Dispose();

        int SaveChanges();
    }
}
