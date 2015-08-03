namespace RecipesHeaven.Web.ViewModels.Home
{
    using AutoMapper;
    using RecipesHeaven.Models;
    using RecipesHeaven.Web.Infrastructure.Mapping;

    public class IndexRecipesViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AuthorName { get; set; }

        public string Category { get; set; }

        public int NumberOfComments { get; set; }

        public int? ImageId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Recipe, IndexRecipesViewModel>()
                .ForMember(vm => vm.Category, op => op.MapFrom(r => r.Category.Name))
                .ForMember(vm => vm.NumberOfComments, op => op.MapFrom(r => r.Comments.Count))
                .ForMember(vm => vm.AuthorName, op => op.MapFrom(r => r.Author.UserName));
        }
    }
}