using Denifia.Stardew.BuyRecipes.Core.Domain;
using System.Collections.Generic;

namespace Denifia.Stardew.BuyRecipes.Core.Framework
{
    public interface IModHelper
    {
        List<GameItem> GameObjects { get; }
    }
}
