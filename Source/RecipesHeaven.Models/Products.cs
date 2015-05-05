namespace RecipesHeaven.Models
{
    using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

    public class Product
    {
        private ICollection<Recipe> recipes;

        public Product()
        {
            this.Recipes = new HashSet<Recipe>();
        }
        
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public int Quantity { get; set; }

        public virtual QuantityType QyantitiType { get; set; }

        public virtual ICollection<Recipe> Recipes
        {
            get { return this.recipes; }
            set { this.recipes = value; }
        }
    }
}
