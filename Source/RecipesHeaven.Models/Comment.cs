namespace RecipesHeaven.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public virtual User Author { get; set; }

        public string Content { get; set; }
    }
}
