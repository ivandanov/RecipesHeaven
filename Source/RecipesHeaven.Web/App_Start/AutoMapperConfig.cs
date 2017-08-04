namespace RecipesHeaven.Web.App_Start
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;

    using AutoMapper;

    using RecipesHeaven.Web.Infrastructure.Mapping;
    using RecipesHeaven.Models;
    using RecipesHeaven.Web.ViewModels.Comment;
    using RecipesHeaven.Web.ViewModels.Recipe;
    using RecipesHeaven.Web.Infrastructure.ImageProcessing;
    using RecipesHeaven.Web.ViewModels.Category;
    using RecipesHeaven.Web.ViewModels.Product;

    public static class AutoMapperConfig
    {
        public static void Execute()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<Category, CategoryViewModel>()
                    .ForMember(vm => vm.NumberRecipes, op => op.MapFrom(mc => mc.Recipes.Count()));
                cfg.CreateMap<Comment, CommentViewModel>()
                    .ForMember(vm => vm.AuthorId, op => op.MapFrom(r => r.Author.Id))
                    .ForMember(vm => vm.AuthorName, op => op.MapFrom(r => r.Author.UserName));
                cfg.CreateMap<Recipe, RecipeOverviewModel>()
                    .ForMember(vm => vm.NumberOfComments, op => op.MapFrom(r => r.Comments.Count))
                    .ForMember(vm => vm.Rating, op => op.MapFrom(r => Convert.ToInt32(r.Rating.Average(l => l.Value))))
                    .ForMember(vm => vm.ImageUrl, op => op.MapFrom(r => "" + r.ImageUrl));
                cfg.CreateMap<Recipe, RecipeOverviewModel>()
                    .ForMember(vm => vm.NumberOfComments, op => op.MapFrom(r => r.Comments.Count))
                    .ForMember(vm => vm.Rating, op => op.MapFrom(r => Convert.ToInt32(r.Rating.Average(l => l.Value))))
                    .ForMember(vm => vm.ImageUrl, op => op.MapFrom(r => ImageConfiguration.UploadedImagesPath.TrimStart('~') + r.ImageUrl));
                cfg.CreateMap<Recipe, RecipeViewModel>()
                    .ForMember(vm => vm.AuthorId, op => op.MapFrom(r => r.Author.Id))
                    .ForMember(vm => vm.AuthorName, op => op.MapFrom(r => r.Author.UserName))
                    .ForMember(vm => vm.CategoryId, op => op.MapFrom(r => r.Category.Id))
                    .ForMember(vm => vm.CategoryName, op => op.MapFrom(r => r.Category.Name))
                    .ForMember(vm => vm.NumberOfComments, op => op.MapFrom(r => r.Comments.Count))
                    .ForMember(vm => vm.Rating, op => op.MapFrom(r => Convert.ToInt32(r.Rating.Average(l => l.Value))))
                    .ForMember(vm => vm.ImageUrl, op => op.MapFrom(r => ImageConfiguration.UploadedImagesPath.TrimStart('~') + r.ImageUrl));
            });

            //var test = Mapper.Configuration.GetAllTypeMaps();
            //var types = Assembly.GetExecutingAssembly().GetExportedTypes();
            //LoadStandardMappings(types);
            //LoadCustomMappings(types);
        }

        private static void LoadStandardMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where
                            i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                            !t.IsAbstract &&
                            !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();

            foreach (var map in maps)
            {
                Mapper.Initialize(cfg => cfg.CreateMap(map.Source.GetType(), map.Destination.GetType()));
                Mapper.Map(map.Source, map.Destination);
            }
        }

        private static void LoadCustomMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                        where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select (IHaveCustomMappings)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings();
            }

            var test = Mapper.Configuration.GetAllTypeMaps();
        }
    }
}