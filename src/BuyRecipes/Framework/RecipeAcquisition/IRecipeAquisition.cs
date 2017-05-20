namespace Denifia.Stardew.BuyRecipes.Framework.RecipeAcquisition
{
    public interface IRecipeAcquisition
    {
        static bool AcceptsConditions(string condition);
        int Cost { get; }
    }
}
