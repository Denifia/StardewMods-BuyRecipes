using System.Collections.Generic;
using Denifia.Stardew.BuyRecipes.Core.Domain;

namespace Denifia.Stardew.BuyRecipes.Core.Adapters
{
    public interface IGameObjectsAdapter
    {
        List<GameItem> GameObjects { get; }
    }
}