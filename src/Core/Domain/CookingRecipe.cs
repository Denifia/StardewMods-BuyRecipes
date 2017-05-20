using Denifia.Stardew.BuyRecipes.Core.Framework;
using Denifia.Stardew.BuyRecipes.Framework;
using System.Collections.Generic;
using System.Reflection;

namespace Denifia.Stardew.BuyRecipes.Core.Domain
{
    public class CookingRecipe
    {
        private string _name;
        private IEnumerable<GameItemWithQuantity> _ingredients;
        private GameItemWithQuantity _resultingItem;
        private int _cost;

        public string Name => _name;
        public IEnumerable<GameItemWithQuantity> Ingredients => _ingredients;
        public GameItemWithQuantity ResultingItem => _resultingItem;
        public int Cost => _cost;

        public static CookingRecipe Deserialise(string name, string data, IModHelper modHelper)
        {
            var cookingRecipeData = CookingRecipeData.Deserialise(data);
            var ingredients = IngredientFactory.DeserializeIngredients(cookingRecipeData.IngredientsData, modHelper);
            var resultingItem = IngredientFactory.DeserializeIngredient(cookingRecipeData.ResultingItemData, modHelper);
            var cost = RecipePricingFactory.CalculatePrice(cookingRecipeData.AcquisitionData);
            return new CookingRecipe(name, ingredients, resultingItem, cost);
        }

        public CookingRecipe(string name, 
            IEnumerable<GameItemWithQuantity> ingredients, 
            GameItemWithQuantity resultingItem,
            int cost)
        {
            _name = name;
            _ingredients = ingredients;
            _resultingItem = resultingItem;
            _cost = cost;
        }
    }

    
}
