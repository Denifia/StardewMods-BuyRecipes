namespace Denifia.Stardew.BuyRecipes.Core.Framework.RecipePricing
{
    internal class SkillBasedRecipePricing : BaseRecipePricing
    {
        protected static readonly new string _deserialisationToken = "s ";

        private readonly int _pricePerLevel = 900;

        private readonly int _skillLevel;
        internal int SkillLevel => _skillLevel;
        
        public static new SkillBasedRecipePricing Deserialise(string data)
        {
            var dataParts = data.Split(' ');
            var skill = dataParts[1];
            var skillLevel = int.Parse(dataParts[2]);

            return new SkillBasedRecipePricing(skillLevel);
        }

        public override int CalculatePrice()
        {
            return _skillLevel * _pricePerLevel;
        }

        protected SkillBasedRecipePricing(int skillLevel)
        {
            _skillLevel = skillLevel;
        }
    }
}
