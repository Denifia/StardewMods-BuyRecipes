using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denifia.Stardew.BuyRecipes.Core.Smapi
{
    //
    // Summary:
    //     The log severity levels.
    public enum LogLevel
    {
        //
        // Summary:
        //     Tracing info intended for developers.
        Trace = 0,
        //
        // Summary:
        //     Troubleshooting info that may be relevant to the player.
        Debug = 1,
        //
        // Summary:
        //     Info relevant to the player. This should be used judiciously.
        Info = 2,
        //
        // Summary:
        //     An issue the player should be aware of. This should be used rarely.
        Warn = 3,
        //
        // Summary:
        //     A message indicating something went wrong.
        Error = 4,
        //
        // Summary:
        //     Important information to highlight for the player when player action is needed
        //     (e.g. new version available). This should be used rarely to avoid alert fatigue.
        Alert = 5
    }
}
