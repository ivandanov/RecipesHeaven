using AutoMapper;
using RecipesHeaven.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipesHeaven.Web.ViewModels.Recipe
{
    public class RecipeViewModel : IMapFrom<Models.Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AuthorName { get; set; }

        public string Category { get; set; }

        public int NumberOfComments { get; set; }

        public int? ImageId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Models.Recipe, RecipeViewModel>()
                .ForMember(vm => vm.Category, op => op.MapFrom(r => r.Category.Name))
                .ForMember(vm => vm.NumberOfComments, op => op.MapFrom(r => r.Comments.Count))
                .ForMember(vm => vm.AuthorName, op => op.MapFrom(r => r.Author.UserName));
        }
    }
}