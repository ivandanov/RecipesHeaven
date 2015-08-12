using RecipesHeaven.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecipesHeaven.Models;
using AutoMapper;

namespace RecipesHeaven.Web.ViewModels.Category
{
    public class CategoryViewModel : IMapFrom<Models.Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberRecipes { get; set; }

        public ICollection<Models.Recipe> Recipes { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Models.Category, CategoryViewModel>()
                .ForMember(vm => vm.NumberRecipes, op => op.MapFrom(mc => mc.Recipes.Count()));
        }
    }
}