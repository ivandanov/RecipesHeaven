namespace RecipesHeaven.Web.ViewModels.Recipe
{
    using AutoMapper;
    using RecipesHeaven.Web.Infrastructure.Mapping;

    public class RecipeCommentViewModel : IMapFrom<Models.Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string Content { get; set; }

        public int RecipeId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Models.Comment, RecipeCommentViewModel>()
                .ForMember(vm => vm.AuthorId, op => op.MapFrom(r => r.Author.Id))
                .ForMember(vm => vm.AuthorName, op => op.MapFrom(r => r.Author.UserName));
        }
    }
}