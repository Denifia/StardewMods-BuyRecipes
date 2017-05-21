using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denifia.Stardew.BuyRecipes.Core.Converters
{
    public class MoneyConverter
    {
        public static string GetMoneyAsString(int money) => $"G{money.ToString("#,##0")}";
    }
}
