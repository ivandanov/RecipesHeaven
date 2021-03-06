﻿namespace RecipesHeaven.Services.Contracts
{
    using System.Linq;
    using System.Collections.Generic;

    using RecipesHeaven.Models;
    
    public interface IProductService
    {
        IQueryable<Product> GetMostUsedProducts(int numberOfProducts);

        IQueryable<Product> GetMostUnusedProducts(int numberOfProducts);

        IQueryable<Product> GetProductsByName(string name);
    }
}
