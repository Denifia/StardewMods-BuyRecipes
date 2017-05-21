using Denifia.Stardew.BuyRecipes.Core.Adapters;
using StardewModdingAPI;

namespace Denifia.Stardew.BuyRecipes.Adapters
{
    internal class SmapiMonitorAdapter : ISmapiMonitorAdapter
    {
        IMonitor _monitor;

        public SmapiMonitorAdapter(IMonitor monitor)
        {
            _monitor = monitor;
        }

        public void Log(string message, Core.Smapi.LogLevel level = Core.Smapi.LogLevel.Debug)
        {
            _monitor.Log(message, (LogLevel)level);
        }
    }
}
