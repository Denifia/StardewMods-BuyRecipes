﻿using Denifia.Stardew.BuyRecipes.Core.Framework.RecipePricing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Denifia.Stardew.BuyRecipes.Framework
{
    internal static class RecipePricingFactory
    {
        private static IEnumerable<Type> _recipeAcquisitionTypes = typeof(RecipePricingFactory).Assembly
                .GetTypes().Where(p => p.IsSubclassOf(typeof(BaseRecipePricing)));

        private delegate bool TryCalculatePrice(string data, out int cost);

        public static int CalculatePrice(string conditions)
        {
            foreach (var type in _recipeAcquisitionTypes)
            {
                var tryCalculatePrice = (TryCalculatePrice)Delegate.CreateDelegate(typeof(TryCalculatePrice), type.GetMethod("TryCalculatePrice"));
                var success = tryCalculatePrice(conditions, out int cost);
                if (success) return cost;
            }

            BaseRecipePricing.TryCalculatePrice(string.Empty, out int defaultCost);
            return defaultCost;
        }
    }
}
