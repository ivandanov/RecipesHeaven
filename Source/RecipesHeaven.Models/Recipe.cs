namespace RecipesHeaven.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Recipe
    {
        private ICollection<Product> products;
        private ICollection<byte> rating; 
        
        public Recipe()
        {
            this.Products = new HashSet<Product>();
            this.Rating = new HashSet<byte>(); 
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string PreparingSteps { get; set; }

        public virtual Image Image { get; set; }

        public virtual User Author { get; set; }

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
    }
}
