using Denifia.Stardew.BuyRecipes.Core.Smapi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denifia.Stardew.BuyRecipes.Core.Adapters
{
    public interface ISmapiMonitorAdapter
    {
        void Log(string message, LogLevel level = LogLevel.Debug);
    }
}
