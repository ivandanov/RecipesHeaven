namespace RecipesHeaven.Web.ViewModels.Product
{
    using AutoMapper;

    using RecipesHeaven.Web.Infrastructure.Mapping;

    public class ProductViewModel : IMapFrom<Models.Product>
    {
        public string Content { get; set; }
        
        public void CreateMappings()
        {
            //configuration.CreateMap<Models.Product, ProductViewModel>()
            //    .ForMember(vm => vm.QyantitiType, op => op.MapFrom(pr => pr.QyantitiType.ToString()));
        }
    }
}