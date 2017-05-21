using Denifia.Stardew.BuyRecipes.Core.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Denifia.Stardew.BuyRecipes.Core.Domain
{
    /// <summary>
    /// Immutable GameItem with a quantity.
    /// </summary>
    public class GameItemWithQuantity : GameItem
    {
        /// <summary>
        /// Deserialises a GameItemWithQuantity from a string.
        /// </summary>
        /// <param name="data">The serialised GameItemWithQuantity.</param>
        /// <param name="gameObjects">Available game objects.</param>
        /// <returns>The deserialised GameItemWithQuantity.</returns>
        public static new GameItemWithQuantity Deserialise(string data, IEnumerable<GameItem> gameObjects)
        {
            var dataParts = data.Split(' ');
            if (dataParts.Length != 2) return null;
            if (!int.TryParse(dataParts[0], out int id)) return null;
            if (!int.TryParse(dataParts[1], out int quantity)) return null;

            var gameItem = gameObjects.FirstOrDefault(x => x.Id == id);
            if (gameItem == null) return null;

            return new GameItemWithQuantity(id, gameItem.Name, quantity);
        }

        private int _quantity;

        public int Quantity => _quantity;

        public GameItemWithQuantity(int id, string name, int quantity) : base(id, name)
        {
            _quantity = quantity;
        }
    }
}
