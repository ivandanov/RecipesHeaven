namespace RecipesHeaven.Services
{
    using RecipesHeaven.Data.Contracts;

    public class BaseService
    {
        public BaseService(IRecipesHeavenData data)
        {
            this.Data = data;
        }

        protected IRecipesHeavenData Data { get; private set; }
    }
}
