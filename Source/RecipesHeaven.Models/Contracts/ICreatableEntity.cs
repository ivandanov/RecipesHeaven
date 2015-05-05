namespace RecipesHeaven.Models.Contracts
{
    using System;

    public interface ICreatableEntity
    {
        DateTime CreatedOn { get; set; }

        bool PreserveCreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
