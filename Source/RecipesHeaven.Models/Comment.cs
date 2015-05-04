namespace RecipesHeaven.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        private ICollection<byte> rating;

        public Comment()
        {
            this.Rating = new HashSet<byte>();
        }

        [Key]
        public int Id { get; set; }

        public virtual User Author { get; set; }

        public string Content { get; set; }

        public virtual ICollection<byte> Rating
        {
            get { return this.rating; }
            set { this.rating = value; }
        }
    }
}
