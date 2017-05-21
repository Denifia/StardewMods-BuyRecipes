﻿using Denifia.Stardew.BuyRecipes.Framework;
using Denifia.Stardew.BuyRecipes.Services;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Denifia.Stardew.BuyRecipes.Core.Domain;
using Autofac;
using Denifia.Stardew.BuyRecipes.Adapters;
using Denifia.Stardew.BuyRecipes.Core.Adapters;
using Denifia.Stardew.BuyRecipes.Core.Framework;
using Denifia.Stardew.BuyRecipes.Core.Converters;

namespace Denifia.Stardew.BuyRecipes
{
    /// <summary>The mod entry point.</summary>
    public class BuyRecipesMod : Mod
    {
        /*********
        ** Properties
        *********/
        /// <summary>The mod configuration.</summary>
        private Config _config;
        private IGameObjectsAdapter _gameObjectsAdapter;

        /*********
        ** State
        *********/
        private bool _savedGameLoaded = false;
        private List<CookingRecipe> _unknownCookingRecipes;
        private List<CookingRecipe> _thisWeeksRecipes;
        private int _seed;
        

        /*********
        ** Public methods
        *********/
        /// <summary>
        /// Entry point for the mod.
        /// </summary>
        /// <param name="helper">The SMAPI injected mod helper.</param>
        public override void Entry(IModHelper helper)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(this).As<IMod>();
            builder.RegisterInstance(helper).As<IModHelper>();
            builder.RegisterInstance(this.Monitor).As<IMonitor>();

            var conventions = new string[] { "Service", "Helper", "Adapter" };
            builder.RegisterAssemblyTypes(typeof(BuyRecipesMod).Assembly)
                .Where(t => conventions.Any(c => t.Name.EndsWith(c)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();
            builder.RegisterAssemblyTypes(typeof(ErrorHelper).Assembly)
                .Where(t => conventions.Any(c => t.Name.EndsWith(c)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            var container = builder.Build();

            _gameObjectsAdapter = container.Resolve<IGameObjectsAdapter>();

            _config = helper.ReadConfig<Config>();

            SaveEvents.AfterLoad += SaveEvents_AfterLoad;
            SaveEvents.AfterReturnToTitle += SaveEvents_AfterReturnToTitle;
            TimeEvents.DayOfMonthChanged += TimeEvents_DayOfMonthChanged;

            helper.ConsoleCommands
                .Add("buyrecipe", $"Buy a recipe. \n\nUsage: buyrecipe \"<name of recipe>\" \n\nNote: This is case sensitive!", HandleCommand)
                .Add("showrecipes", $"Lists this weeks available recipes. \n\nUsage: showrecipes", HandleCommand);

#if (DEBUG)
            helper.ConsoleCommands.Add("buyallrecipes", $"Temporary. \n\nUsage: buyallrecipes", HandleCommand);
#endif

            // Instance the Version Check Service
            new VersionCheckService(this);
        }

        /*********
        ** Event methods
        *********/

        private void SaveEvents_AfterReturnToTitle(object sender, EventArgs e)
        {
            _savedGameLoaded = false;
            _unknownCookingRecipes = null;
            _thisWeeksRecipes = null;
        }
        
        private void SaveEvents_AfterLoad(object sender, EventArgs e)
        {
            _savedGameLoaded = true;
            FindUnknownRecipes();
            TryGenerateWeeklyRecipes();
        }

        private void TimeEvents_DayOfMonthChanged(object sender, EventArgsIntChanged e) => TryGenerateWeeklyRecipes();


        /*********
        ** Private methods
        *********/

        private void HandleCommand(string command, string[] args)
        {
            args = new List<string>() { string.Join(" ", args) }.ToArray();

            if (!_savedGameLoaded)
            {
                Monitor.Log("Please load up a saved game first, then try again.", LogLevel.Warn);
                return;
            }

            switch (command)
            {
                case "buyrecipe":
                    BuyRecipe(args);
                    break;
                case "showrecipes":
                    TryShowWeeklyRecipes();
                    break;
                case "buyallrecipes":
                    _unknownCookingRecipes.ForEach(recipe => BuyRecipe(new string[] { recipe.Name }, false));
                    break;
                default:
                    throw new NotImplementedException($"Send Items received unknown command '{command}'.");
            }
        }

        private void BuyRecipe(string[] args, bool checkInWeeklyRecipes = true)
        {
            if (args.Length == 1)
            {
                var recipeName = args[0].Trim('"');
                var recipe = _unknownCookingRecipes.FirstOrDefault(x => x.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
                if (recipe == null)
                {
                    Monitor.Log("Recipe not found", LogLevel.Info);
                    return;
                }

                // Use the explicit name
                recipeName = recipe.Name;

                if (Game1.player.cookingRecipes.ContainsKey(recipeName))
                {
                    Monitor.Log("Recipe already known", LogLevel.Info);
                    return;
                }

                if (checkInWeeklyRecipes && !_thisWeeksRecipes.Any(x => x.Name.Equals(recipeName)))
                {
                    Monitor.Log("Recipe is not availble to buy this week", LogLevel.Info);
                    return;
                }

                if (Game1.player.Money < recipe.Cost)
                {
                    Monitor.Log("You can't affort this recipe", LogLevel.Info);
                    return;
                }

                Game1.player.cookingRecipes.Add(recipeName, 0);
                Game1.player.Money -= recipe.Cost;
                _unknownCookingRecipes.Remove(recipe);
                Monitor.Log($"{recipeName} bought for {MoneyConverter.GetMoneyAsString(recipe.Cost)}!", LogLevel.Alert);
            }
            else
            {
                LogArgumentsInvalid("buy");
            }
        }

        /// <summary>Generate a new set of weekly recipes if it's a new week.</summary>
        private void TryGenerateWeeklyRecipes()
        {
            // Check if it's time for a new weekly week
            {
                var gameDateTime = GameDateTime.Deserialise(Game1.timeOfDay, Game1.dayOfMonth, Game1.currentSeason, Game1.year);
                var startOfWeek = gameDateTime.StartOfWeek;
                var seed = int.Parse($"{startOfWeek.DayOfMonth}{startOfWeek.Season}{startOfWeek.Year}");
                if (_seed == seed) return;
                _seed = seed;
            }

            // Reset the current recipe list
            _thisWeeksRecipes = new List<CookingRecipe>();

            // Check if there is any unknown recipes
            if (TryShowNoRecipes(_unknownCookingRecipes)) return;

            // Find up to 5 random recipes
            {
                var random = new Random(_seed);
                for (int i = 0; i < _config.MaxNumberOfRecipesPerWeek; i++)
                {
                    var recipe = _unknownCookingRecipes[random.Next(_unknownCookingRecipes.Count)];

                    // If recipe is not already in the list, add it
                    if (!_thisWeeksRecipes.Any(x => x.Name.Equals(recipe.Name)))
                        _thisWeeksRecipes.Add(recipe);
                }
            }

            TryShowWeeklyRecipes();
        }

        /// <summary>Find all the unknown recipes for the player.</summary>
        private void FindUnknownRecipes()
        {
            _unknownCookingRecipes = new List<CookingRecipe>();
            foreach (var recipe in CraftingRecipe.cookingRecipes)
            {
                var cookingRecipe = CookingRecipe.Deserialise(recipe.Key, recipe.Value, _gameObjectsAdapter.GameObjects);
                if (Game1.player.cookingRecipes.ContainsKey(cookingRecipe.Name))
                    _unknownCookingRecipes.Add(cookingRecipe);
            }
        }

        /// <summary>Writes the weekly recipes or a No Recipes message to the console.</summary>
        /// <returns>True if the weekly recipes are written to the console.</returns>
        private bool TryShowWeeklyRecipes()
        {
            // Check if there is any unknown recipes 
            if (TryShowNoRecipes(_thisWeeksRecipes)) return false;

            // Print out the weekly recipes to the console
            Monitor.Log($"This weeks recipes are:", LogLevel.Alert);
            _thisWeeksRecipes.ForEach(item => Monitor.Log($"{MoneyConverter.GetMoneyAsString(item.Cost)} - {item.Name}", LogLevel.Info));
            return true;
        }

        /// <summary>Writes a No Recipes message to the console if no recipes are found.</summary>
        /// <returns>True if no recipes are found and the message is written to the console.</returns>      
        private bool TryShowNoRecipes(List<CookingRecipe> recipes)
        {
            if (recipes.Any()) return false;

            Monitor.Log($"No recipes availabe. You know them all.", LogLevel.Info);
            return true;
        }

        private void LogUsageError(string error, string command)
        {
            Monitor.Log($"{error} Type 'help {command}' for usage.", LogLevel.Error);
        }

        private void LogArgumentsInvalid(string command)
        {
            LogUsageError("The arguments are invalid.", command);
        }
    }
}
