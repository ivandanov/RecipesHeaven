namespace RecipesHeaven.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Recipe
    {
        private ICollection<Product> products;
        private ICollection<Comment> comments;
        private ICollection<byte> rating;
        
        public Recipe()
        {
            this.Products = new HashSet<Product>();
            this.Rating = new HashSet<byte>();
            this.Comments = new HashSet<Comment>(); 
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string PreparingSteps { get; set; }

        public virtual Image Image { get; set; }

        public virtual User Author { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        public virtual ICollection<Product> Products 
        {
            get { return this.products; } 
            set { this.products = value; }
        }

        public virtual ICollection<byte> Rating
        {
            get { return this.rating; }
            set { this.rating = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
