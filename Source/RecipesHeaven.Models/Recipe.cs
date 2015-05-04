namespace RecipesHeaven.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Recipe
    {
        private ICollection<Products> products;
        private ICollection<byte> rating;
        
        public Recipe()
        {
            this.Products = new HashSet<Products>();
            this.Rating = new HashSet<byte>();
        }

        [Key]
        public int Id { get; set; }

        public virtual Image Image { get; set; }

        public virtual User Author { get; set; }

        public virtual ICollection<Products> Products 
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
