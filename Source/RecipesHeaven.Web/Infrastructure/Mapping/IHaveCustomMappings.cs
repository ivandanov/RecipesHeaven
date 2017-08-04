namespace RecipesHeaven.Web.Infrastructure.Mapping
{
    using AutoMapper;
    using AutoMapper.Configuration;

    public interface IHaveCustomMappings
    {
        void CreateMappings();
    }
}