using Denifia.Stardew.BuyRecipes.Core.Adapters;
using System.Linq;

namespace Denifia.Stardew.BuyRecipes.Core.Services
{
    public class GameItemNameService : IGameItemNameService
    {
        private readonly IGameObjectsAdapter _gameObjects;

        public string GetNameById(int id)
        {
            var foundGameItem = _gameObjects?.GameObjects?.FirstOrDefault(x => x.Id == id);
            return foundGameItem?.Name;
        }
    }
}
