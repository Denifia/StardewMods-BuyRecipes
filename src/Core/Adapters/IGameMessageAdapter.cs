namespace Denifia.Stardew.BuyRecipes.Core.Adapters
{
    public interface IGameMessageAdapter
    {
        void ShowErrorMessage(string message);
        void ShowInfoMessage(string message, int? duration = default(int?));
    }
}