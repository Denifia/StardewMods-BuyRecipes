namespace Denifia.Stardew.BuyRecipes.Core.Framework.RecipePricing
{
    internal class FriendBasedRecipePricing : BaseRecipePricing
    {
        protected static readonly new string _deserialisationToken = "f ";
        private readonly int _pricePerLevel = 600;
        private readonly int _friendLevel;

        public static new FriendBasedRecipePricing Deserialise(string data)
        {
            var dataParts = data.Split(_delimiter);
            var friend = dataParts[1];
            var friendLevel = int.Parse(dataParts[2]);

            return new FriendBasedRecipePricing(friendLevel);
        }

        public override int CalculatePrice()
        {
            return _friendLevel * _pricePerLevel;
        }

        protected FriendBasedRecipePricing(int friendLevel)
        {
            _friendLevel = friendLevel;
        }
    }
}
