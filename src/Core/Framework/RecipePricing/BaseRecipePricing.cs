namespace Denifia.Stardew.BuyRecipes.Core.Framework.RecipePricing
{
    internal class BaseRecipePricing : IRecipePricing
    {
        protected static readonly string _deserialisationToken = "";
        protected static readonly char _delimiter = ' ';
        protected readonly int _defaultPrice = 1000;        

        protected BaseRecipePricing() { }

        public static bool CanDeserialise(string data)
        {
            if (string.IsNullOrEmpty(data) || !data.StartsWith(_deserialisationToken)) return false;
            return true;
        }

        public static BaseRecipePricing Deserialise(string data)
        {
            return new BaseRecipePricing();
        }

        public virtual int CalculatePrice()
        {
            return _defaultPrice;
        }
    }
}
