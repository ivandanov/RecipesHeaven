namespace RecipesHeaven.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using AutoMapper;

    using RecipesHeaven.Models;
    using RecipesHeaven.Web.Infrastructure.Mapping;
    using RecipesHeaven.Web.ViewModels.Recipe;

    public class IndexViewModel 
    {
        public IndexViewModel()
        {
            this.CarouselRecipes = new List<RecipeOverviewModel>();
            this.NewestRecipes = new List<RecipeOverviewModel>();
        }

        public IList<RecipeOverviewModel> CarouselRecipes { get; set; }

        public IList<RecipeOverviewModel> NewestRecipes { get; set; }
    }
}