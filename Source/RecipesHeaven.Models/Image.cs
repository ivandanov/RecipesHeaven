using System.ComponentModel.DataAnnotations.Schema;

namespace RecipesHeaven.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Image
    {
        [Key]
        public int Id { get; set; }

        public byte[] Content { get; set; }

        public string FileExtension { get; set; }

        //public int RecipeId { get; set; }

        //public virtual Recipe Recipe { get; set; }
    }
}
