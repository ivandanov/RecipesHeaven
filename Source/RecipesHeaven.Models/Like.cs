namespace RecipesHeaven.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Like
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public byte Value { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
