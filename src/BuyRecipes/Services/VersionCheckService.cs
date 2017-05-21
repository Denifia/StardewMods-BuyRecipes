using Denifia.Stardew.BuyRecipes.Core.Adapters;
using Denifia.Stardew.BuyRecipes.Core.Framework;
using Denifia.Stardew.BuyRecipes.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using System;

namespace Denifia.Stardew.BuyRecipes.Services
{
    public class VersionCheckService
    {
        private IGameMessageAdapter _gameMessage;
        private IErrorHelper _errorHelper;
        private IMod _mod;
        private Config _config;
        private ISemanticVersion _currentVersion;
        private ISemanticVersion _newRelease;
        private bool _hasSeenUpdateWarning;

        public VersionCheckService(IMod mod)
        {
            _mod = mod;

            _config = mod.Helper.ReadConfig<Config>();
            _currentVersion = mod.ModManifest.Version;

            GameEvents.GameLoaded += GameEvents_GameLoaded;
            SaveEvents.AfterLoad += SaveEvents_AfterLoad;
        }

        private async void GameEvents_GameLoaded(object sender, EventArgs e)
        {
            // check for mod update
            if (_config.CheckForUpdates && !_config.Debug)
            {
                try
                {
                    ISemanticVersion latest = await UpdateHelper.LogVersionCheck(_mod.Monitor, _mod.ModManifest.Version);
                    if (latest.IsNewerThan(_currentVersion))
                    {
                        _newRelease = latest;
                    }
                }
                catch (Exception ex)
                {
                    _errorHelper.HandleError(ex, "checking for a new version");
                }
            }
        }

        private void SaveEvents_AfterLoad(object sender, EventArgs e)
        {
            // render update warning
            if (_config.CheckForUpdates && !_hasSeenUpdateWarning && _newRelease != null)
            {
                try
                {
                    _hasSeenUpdateWarning = true;
                    _gameMessage.ShowInfoMessage($"You can update {Constants.ModName} from {_currentVersion} to {_newRelease}.");
                }
                catch (Exception ex)
                {
                    _errorHelper.HandleError(ex, "showing the new version available");
                }
            }
        }
    }
}
