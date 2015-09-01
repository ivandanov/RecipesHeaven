namespace RecipesHeaven.Services
{
    using System.Linq;

    using RecipesHeaven.Models;
    using RecipesHeaven.Data.Contracts;
    using RecipesHeaven.Services.Contracts;

    public class ProductService : BaseService, IProductService
    {
        public ProductService(IRecipesHeavenData data)
            :base(data)
        {
        }

        public IQueryable<Product> GetMostUsedProducts(int numberOfProducts)
        {
            return this.Data
                .Products
                .All()
                .OrderByDescending(p => p.Recipes.Count);
        }

        public IQueryable<Product> GetMostUnusedProducts(int numberOfProducts)
        {
            return this.Data
                .Products
                .All()
                .OrderBy(p => p.Recipes.Count);
        }

        public IQueryable<Product> GetProductsByName(string name)
        {
            return this.Data
                .Products
                .All()
                .Where(p => p.Content.Contains(name))
                .OrderBy(p => p.Content.Length);
        }
    }
}
