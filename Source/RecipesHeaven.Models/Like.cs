namespace RecipesHeaven.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Like
    {
        [NotMapped]
        public static byte DefaultRatingValue { get { return 3; } }

        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public byte Value { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
