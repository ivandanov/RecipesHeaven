namespace RecipesHeaven.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using RecipesHeaven.Models.Contracts;

    public class Comment : CreatableEntity, ICreatableEntity
    {
        [Key]
        public int Id { get; set; }

        public virtual User Author { get; set; }

        [MaxLength(500)]
        public string Content { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
