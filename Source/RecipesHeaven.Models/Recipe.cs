using System.ComponentModel.DataAnnotations.Schema;

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
        private ICollection<Like> rating;
        
        public Recipe()
        {
            this.Products = new HashSet<Product>();
            this.Rating = new HashSet<Like>();
            this.Comments = new HashSet<Comment>(); 
        }

        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }
        
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string PreparingSteps { get; set; }

        //[ForeignKey("Image")]
        //[Column(Order = 1)]
        //public int? ImageId { get; set; }
        //public virtual Image Image { get; set; }

        public string ImageUrl { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual DateTime DateAdded { get; set; }

        [Required]
        public virtual ICollection<Product> Products 
        {
            get { return this.products; } 
            set { this.products = value; }
        }

        public virtual ICollection<Like> Rating
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
