using AutoMapper;
using RecipesHeaven.Web.Infrastructure.ImageProcessing;
using RecipesHeaven.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace RecipesHeaven.Web.ViewModels.Recipe
{
    public class RecipeOverviewModel : BaseViewModel, IMapFrom<Models.Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfComments { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            var uploadedImagePath = ImageConfiguration.UploadedImagesPath.TrimStart('~');

            configuration.CreateMap<Models.Recipe, RecipeOverviewModel>()
                .ForMember(vm => vm.NumberOfComments, op => op.MapFrom(r => r.Comments.Count))
                .ForMember(vm => vm.ImageUrl, op => op.MapFrom(r => uploadedImagePath + r.ImageUrl));
        }
    }
}