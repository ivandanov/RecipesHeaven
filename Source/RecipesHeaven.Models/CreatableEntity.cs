namespace RecipesHeaven.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using RecipesHeaven.Models.Contracts;

    public abstract class CreatableEntity : ICreatableEntity
    {
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Specifies whether or not the CreatedOn property should be automatically set.
        /// </summary>
        [NotMapped]
        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
