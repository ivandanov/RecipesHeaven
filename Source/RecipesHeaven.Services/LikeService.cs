namespace RecipesHeaven.Services
{
    using System;
    using System.Linq;

    using RecipesHeaven.Models;
    using RecipesHeaven.Data.Contracts;
    using RecipesHeaven.Services.Contracts;
    using RecipesHeaven.Common;

    public class LikeService : BaseService
    {
        private const int LikeMinValue = 1;
        private const int LikeMaxValue = 5;

        private IRecipeService recipeService;
        private UserService userService;

        public LikeService(IRecipesHeavenData data, IRecipeService recipeService, UserService userService)
            : base(data)
        {
            this.recipeService = recipeService;
            this.userService = userService;
        }

        public void LikeRecipe(int recipeId, string userId, int value)
        {
            var recipe = this.recipeService.GetRecipeById(recipeId);
            if(recipe == null)
            {
                throw new RecipesHeavenException("Recipe not found!");
            }
            
            var user = this.userService.GetUser(userId);
            if(user == null)
            {
                throw new RecipesHeavenException("User not found!");
            }

            if(recipe.Rating.Any(l => l.UserId == user.Id))
            {
                throw new RecipesHeavenException("You already rate this recipe!");
            }

            if(value < LikeMinValue || value > LikeMaxValue || 
                value < Byte.MinValue || value > Byte.MaxValue)
            {
                throw new RecipesHeavenException("Your rating value is out of range!");
            }

            var actualValue = Convert.ToByte(value);
            var like = new Like()
            {
                RecipeId = recipeId,
                UserId = userId,
                Value = actualValue
            };

            recipe.Rating.Add(like);
            this.Data.SaveChanges();
        }
    }
}
