namespace RecipesHeaven.Common
{
    using System;
    using System.Text;

    public class RandomGenerator : Random
    {
        //Last spaces is for generating strings with short words in it
        private const string DefaultCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890         ";
        
        public string RandomString(int minLength = 5, int maxLength = 50, string characters = DefaultCharacters)
        {
            var result = new StringBuilder();
            var length = this.Next(minLength, maxLength);

            for (int i = 0; i <= length; i++)
            {
                result.Append(DefaultCharacters[this.Next(0, DefaultCharacters.Length)]);
            }

            return result.ToString();
        }
    }
}
