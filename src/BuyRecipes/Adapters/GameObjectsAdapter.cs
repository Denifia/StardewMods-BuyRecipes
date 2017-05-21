using Denifia.Stardew.BuyRecipes.Core.Adapters;
using Denifia.Stardew.BuyRecipes.Core.Domain;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denifia.Stardew.BuyRecipes.Adapters
{
    internal class GameObjectsAdapter : IGameObjectsAdapter
    {
        public List<GameItem> GameObjects => _gameObjects ?? (_gameObjects = DeserializeGameObjects().ToList());
        
        private List<GameItem> _gameObjects = new List<GameItem>();
        
        private IEnumerable<GameItem> DeserializeGameObjects()
        {
            foreach (var item in Game1.objectInformation)
            {
                yield return new GameItem(id: item.Key, name: item.Value.Split('/')[4]);
            }
        }
    }
}
