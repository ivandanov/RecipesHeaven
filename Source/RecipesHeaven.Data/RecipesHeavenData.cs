namespace RecipesHeaven.Data
{
    using System;
    using System.Data.Entity;
    using RecipesHeaven.Data.Contracts;
    using RecipesHeaven.Models;
    using System.Collections.Generic;

    public class RecipesHeavenData : IRecipesHeavenData
    {
        private readonly DbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public RecipesHeavenData(DbContext context)
        {
            this.context = context;
        }

        public DbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IRepository<Category> Categories
        {
            get { return this.GetRepository<Category>(); }
        }

        public IRepository<Comment> Comments
        {
            get { return this.GetRepository<Comment>(); }
        }

        public IRepository<Recipe> Recipes
        {
            get { return this.GetRepository<Recipe>(); }
        }

        public IRepository<Product> Products
        {
            get { return this.GetRepository<Product>(); }
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
