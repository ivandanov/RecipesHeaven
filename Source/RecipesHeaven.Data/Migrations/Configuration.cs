namespace RecipesHeaven.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RecipesHeaven.Common;
    using RecipesHeaven.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public sealed class Configuration : DbMigrationsConfiguration<RecipesHeaven.Data.RecipesHeavenDbContext>
    {
        private const int DefaultNumberOfUsers = 10;
        private const int DefaultNumberOfProducts = 20;
        private const int DefaultNumberOfRecipse = 10;
        private const int DefaultNumberOfComments = 10;
        private const string DefaultRecipeImagesPath = "/Migrations/Images/Recipes";
        private readonly string[] DefaultCategories =
            new string[] { "Salads", "Main Dishes", "Fish & Seafood", "Vegetarian", "Desserts", "Beverages" };

        private RandomGenerator random;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.random = new RandomGenerator();
        }

        protected override void Seed(RecipesHeavenDbContext context)
        {
            this.SeedUsers(DefaultNumberOfUsers, context);
            this.SeedProducts(DefaultNumberOfProducts, context);
            this.SeedCategories(DefaultCategories, context);
            this.SeedRecipes(DefaultNumberOfRecipse, context);
            this.SeedComments(DefaultNumberOfComments, context);
        }

        private void SeedUsers(int numberOfUsers, RecipesHeavenDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var userManager = new UserManager<User>(new UserStore<User>(context));

            //create default admin
            var adminUser = new User
            {
                Email = "admin@mysite.com",
                UserName = "Administrator"
            };
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole("Admin"));
            context.SaveChanges();
            userManager.Create(adminUser, "admin123456");
            userManager.AddToRole(adminUser.Id, "Admin");

            //add simle users
            for (int i = 0; i < DefaultNumberOfUsers; i++)
            {
                var user = new User
                {
                    Email = string.Format("{0}@{1}.com", this.random.RandomString(5, 15), this.random.RandomString(5, 15)),
                    UserName = this.random.RandomString(5, 15)
                };

                userManager.Create(user, "123456");
            }
        }

        private void SeedProducts(int numberOfProducts, RecipesHeavenDbContext context)
        {
            if (context.Products.Any())
            {
                return;
            }

            var quantitiTypes = Enum.GetValues(typeof(QuantityType));

            for (int i = 0; i < numberOfProducts; i++)
            {
                var product = new Product()
                {
                    Name = random.RandomString(5, 10),
                    Quantity = random.Next(),
                    QyantitiType = (QuantityType)quantitiTypes.GetValue(random.Next(quantitiTypes.Length))
                };

                context.Products.Add(product);
            }

            context.SaveChanges();
        }

        private void SeedCategories(IEnumerable<string> categoryNames, RecipesHeavenDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            foreach (var catName in categoryNames)
            {
                var category = new Category()
                {
                    Name = catName
                };

                context.Categories.Add(category);
            }

            context.SaveChanges();
        }

        private void SeedRecipes(int numberOfRecipes, RecipesHeavenDbContext context)
        {
            if (context.Recipes.Any())
            {
                return;
            }

            var someUsers = context.Users.Take(DefaultNumberOfUsers).ToList();
            var someProducts = context.Products.Take(DefaultNumberOfProducts).ToList();
            var someCategories = context.Categories.Take(DefaultCategories.Length).ToList();

            for (int i = 0; i < numberOfRecipes; i++)
            {
                var recipe = new Recipe()
                {
                    Name = random.RandomString(10, 49),
                    Author = someUsers[random.Next(0, someUsers.Count)],
                    Category = someCategories[random.Next(0, someCategories.Count)],
                    PreparingSteps = random.RandomString(20, 500),
                    Image = this.GetSampleImage(DefaultRecipeImagesPath),
                    DateAdded = DateTime.Now
                };

                var numberOfProducts = random.Next(1, someProducts.Count);
                recipe.Products = new List<Product>(someProducts.Take(numberOfProducts).ToList());

                context.Recipes.Add(recipe);
            }

            context.SaveChanges();
        }

        private void SeedComments(int numberOfComments, RecipesHeavenDbContext context)
        {
            if (context.Comments.Any())
            {
                return;
            }

            var someUsers = context.Users.Take(DefaultNumberOfUsers).ToList();
            var someRecipes = context.Recipes.Take(DefaultNumberOfRecipse).ToList();

            for (int i = 0; i < numberOfComments; i++)
            {
                var comment = new Comment()
                {
                    Author = someUsers[random.Next(0, someUsers.Count)],
                    Recipe = someRecipes[random.Next(0, someRecipes.Count)],
                    Content = random.RandomString(20, 200)
                };

                context.Comments.Add(comment);
            }

            context.SaveChanges();
        }

        private Image GetSampleImage(string path)
        {
            var directory = this.GetAssemblyDirectory(Assembly.GetExecutingAssembly());
            var allImages = Directory.GetFiles(directory + path);
            var imagePath = allImages[random.Next(0, allImages.Length)];
            var imageExtension = imagePath.Substring(imagePath.LastIndexOf('.'));

            var file = File.ReadAllBytes(imagePath);
            var image = new Image
            {
                Content = file,
                FileExtension = imageExtension
            };

            return image;
        }

        private string GetAssemblyDirectory(Assembly assembly)
        {
            var assemblyLocation = assembly.CodeBase;
            var location = new UriBuilder(assemblyLocation);
            var path = Uri.UnescapeDataString(location.Path);
            var directory = Path.GetDirectoryName(path);
            return directory;
        }
    }
}
