using Denifia.Stardew.BuyRecipes.Core.Domain;
using Denifia.Stardew.BuyRecipes.Core.Services;

namespace Denifia.Stardew.BuyRecipes.Core.ExtensionMethods
{
    public static class CookingRecipeExtensions
    {
        public static CookingRecipe PopulateIngredientsNames(this CookingRecipe cookingRecipe, IGameItemNameService gameItemNameService)
        {
            foreach (var item in cookingRecipe.Ingredients) item.LookupName(gameItemNameService);
            return cookingRecipe;
        }

        public static CookingRecipe PopulateResultingItemName(this CookingRecipe cookingRecipe, IGameItemNameService gameItemNameService)
        {
            cookingRecipe.ResultingItem.LookupName(gameItemNameService);
            return cookingRecipe;
        }
    }
}
