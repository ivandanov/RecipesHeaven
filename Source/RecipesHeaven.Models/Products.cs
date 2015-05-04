namespace RecipesHeaven.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int Quantity { get; set; }

        public virtual QuantityType QyantitiType { get; set; }
    }
}
