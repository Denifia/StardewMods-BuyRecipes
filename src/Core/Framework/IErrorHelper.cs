using System;

namespace Denifia.Stardew.BuyRecipes.Core.Framework
{
    public interface IErrorHelper
    {
        void HandleError(Exception ex, string verb);
    }
}