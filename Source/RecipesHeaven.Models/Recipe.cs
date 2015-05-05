namespace RecipesHeaven.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using RecipesHeaven.Models.Contracts;

    public class Recipe : CreatableEntity, ICreatableEntity
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
        [MaxLength(500)]
        public string PreparingSteps { get; set; }

        public virtual Image Image { get; set; }

        public virtual User Author { get; set; }

        public virtual Category Category { get; set; }

        public virtual DateTime DateAdded { get; set; }

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
