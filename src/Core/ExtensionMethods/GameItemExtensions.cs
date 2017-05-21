using Denifia.Stardew.BuyRecipes.Core.Domain;
using Denifia.Stardew.BuyRecipes.Core.Services;

namespace Denifia.Stardew.BuyRecipes.Core.ExtensionMethods
{
    public static class GameItemExtensions
    {
        public static GameItem LookupName(this GameItem gameItem, IGameItemNameService gameItemNameService)
        {
            gameItem.Name = gameItemNameService.GetNameById(gameItem.Id);
            return gameItem;
        }
    }
}
