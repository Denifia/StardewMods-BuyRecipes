using Denifia.Stardew.BuyRecipes.Core.Adapters;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denifia.Stardew.BuyRecipes.Adapters
{
    internal class GameMessageAdapter : IGameMessageAdapter
    {
        private GameMessageAdapter() { }
        private static GameMessageAdapter instance;
        public static GameMessageAdapter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameMessageAdapter();
                }
                return instance;
            }
        }

        /// <summary>Show an informational message to the player.</summary>
        /// <param name="message">The message to show.</param>
        /// <param name="duration">The number of milliseconds during which to keep the message on the screen before it fades (or <c>null</c> for the default time).</param>
        public void ShowInfoMessage(string message, int? duration = null)
        {
            Game1.addHUDMessage(new HUDMessage(message, 3) { noIcon = true, timeLeft = duration ?? HUDMessage.defaultTime });
        }

        /// <summary>Show an error message to the player.</summary>
        /// <param name="message">The message to show.</param>
        public void ShowErrorMessage(string message)
        {
            Game1.addHUDMessage(new HUDMessage(message, 3));
        }
    }
}
