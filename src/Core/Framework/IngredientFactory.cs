using Denifia.Stardew.BuyRecipes.Core.Domain;
using System.Collections.Generic;

namespace Denifia.Stardew.BuyRecipes.Core.Framework
{
    internal static class IngredientFactory
    {
        /// <summary>
        /// Deserialises a collection of Game Items with Quantities from a string.
        /// </summary>
        /// <param name="data">Serialised string of Game Items in the format of "id quantity id quantity...".</param>
        /// <returns>The deserialised Game Items with Quantities.</returns>
        public static IEnumerable<GameItemWithQuantity> DeserializeIngredients(string data, IEnumerable<GameItem> gameObjects)
        {
            var dataParts = data.Split(' ');
            if (dataParts.Length % 2 != 0) yield break;

            // Iterate in pairs
            for (int i = 0; i < dataParts.Length; i = i+2)
            {
                yield return GameItemWithQuantity.Deserialise($"{dataParts[i]} {dataParts[i + 1]}", gameObjects);
            }
        }

        public static GameItemWithQuantity DeserializeIngredient(string data, IEnumerable<GameItem> gameObjects) 
            => GameItemWithQuantity.Deserialise(data, gameObjects);
    }
}
