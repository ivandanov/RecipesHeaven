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
        public string Content { get; set; }

        //TODO: entities for categorising product in recipe 
        //e.g. "before baking" or "before presenting"

        public virtual ICollection<Recipe> Recipes
        {
            get { return this.recipes; }
            set { this.recipes = value; }
        }
    }
}
