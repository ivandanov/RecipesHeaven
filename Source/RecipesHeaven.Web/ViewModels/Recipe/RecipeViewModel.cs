namespace RecipesHeaven.Web.ViewModels.Recipe
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;

    using RecipesHeaven.Web.Infrastructure.Mapping;
    using RecipesHeaven.Web.ViewModels.Product;
    using RecipesHeaven.Web.ViewModels.Comment;
    using System.Web;
    using RecipesHeaven.Web.Infrastructure.ImageProcessing;

    public class RecipeViewModel : BaseViewModel, IMapFrom<Models.Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int NumberOfComments { get; set; }

        public DateTime DateAdded { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        public virtual ICollection<ProductViewModel> Products { get; set; }

        public string ImageUrl { get; set; }

        public string PreparingSteps { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            var imagePath = HttpContext.Current.Server.MapPath(ImageConfiguration.UploadedImagesPath);

            configuration.CreateMap<Models.Recipe, RecipeViewModel>()
                .ForMember(vm => vm.AuthorId, op => op.MapFrom(r => r.Author.Id))
                .ForMember(vm => vm.AuthorName, op => op.MapFrom(r => r.Author.UserName))
                .ForMember(vm => vm.CategoryId, op => op.MapFrom(r => r.Category.Id))
                .ForMember(vm => vm.CategoryName, op => op.MapFrom(r => r.Category.Name))
                .ForMember(vm => vm.NumberOfComments, op => op.MapFrom(r => r.Comments.Count))
                .ForMember(vm => vm.ImageUrl, op => op.MapFrom(r => imagePath + r.ImageUrl));
        }
    }
}