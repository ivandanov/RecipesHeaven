namespace RecipesHeaven.Common
{
    using System;
    using System.Text;

    public class RandomGenerator : Random
    {
        private const string DefaultCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz 1234567890";
        
        public string RandomString(int minLength = 5, int maxLength = 50, string characters = DefaultCharacters)
        {
            var result = new StringBuilder();
            var length = this.Next(minLength, maxLength + 1);

            for (int i = 0; i <= length; i++)
            {
                result.Append(DefaultCharacters[this.Next(0, DefaultCharacters.Length)]);
            }

            return result.ToString();
        }
    }
}
