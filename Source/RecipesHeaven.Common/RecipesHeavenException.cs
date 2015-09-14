namespace RecipesHeaven.Common
{
    using System;

    public class RecipesHeavenException : Exception
    {
        public RecipesHeavenException(string message)
            : base (message)
        {
        }

        public RecipesHeavenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public RecipesHeavenException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {
        }
    }
}
