using AutoMapper;
using RecipesHeaven.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace RecipesHeaven.Web.ViewModels.Recipe
{
    public class RecipeOverviewModel : IMapFrom<Models.Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int NumberOfComments { get; set; }

        public int? ImageId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            Expression<Func<Models.Recipe, string>> shortDesc =
                r => r.PreparingSteps
                    .Substring(0, r.PreparingSteps.Substring(0, 70).LastIndexOf(' ')) 
                    + "...";
            
            configuration.CreateMap<Models.Recipe, RecipeOverviewModel>()
                .ForMember(vm => vm.NumberOfComments, op => op.MapFrom(shortDesc))
                .ForMember(vm => vm.NumberOfComments, op => op.MapFrom(r => r.Comments.Count));
        }

    }
}