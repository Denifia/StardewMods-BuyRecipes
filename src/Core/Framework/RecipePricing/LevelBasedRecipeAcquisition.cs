namespace Denifia.Stardew.BuyRecipes.Core.Framework.RecipePricing
{
    internal class LevelBasedRecipePricing : BaseRecipePricing
    {
        protected static readonly new string _deserialisationToken = "l ";
        private readonly int _pricePerLevel = 75;
        private readonly int _priceAtLevel100 = 1750;
        private readonly int _level100 = 100;
        private readonly int _playerLevel;

        public static new LevelBasedRecipePricing Deserialise(string data)
        {
            var dataParts = data.Split(' ');
            var playerLevel = int.Parse(dataParts[1]);

            return new LevelBasedRecipePricing(playerLevel);
        }

        public override int CalculatePrice()
        {
            if (_playerLevel == _level100)
                return _priceAtLevel100;

            return _playerLevel * _pricePerLevel;
        }

        protected LevelBasedRecipePricing(int level)
        {
            _playerLevel = level;
        }
    }
}
