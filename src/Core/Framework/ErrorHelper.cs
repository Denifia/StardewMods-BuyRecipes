using Denifia.Stardew.BuyRecipes.Core.Adapters;
using Denifia.Stardew.BuyRecipes.Core.Smapi;
using System;

namespace Denifia.Stardew.BuyRecipes.Core.Framework
{
    public class ErrorHelper : IErrorHelper
    {
        private ISmapiMonitorAdapter _monitor;
        private IGameMessageAdapter _gameMessage;

        public void HandleError(Exception ex, string verb)
        {
            _monitor.Log($"Something went wrong {verb}:\n{ex}", LogLevel.Error);
            _gameMessage.ShowErrorMessage($"Huh. Something went wrong {verb}. The error log has the technical details.");
        }
    }
}
