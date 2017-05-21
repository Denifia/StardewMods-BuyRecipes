namespace Denifia.Stardew.BuyRecipes.Core.Domain
{
    public class GameItem
    {
        /// <summary>
        /// Deserialises a GameItem from a string.
        /// </summary>
        /// <param name="data">The serialised GameItem.</param>
        /// <returns>The deserialised GameItem.</returns>
        public static GameItem Deserialise(string data)
        {
            var dataParts = data.Split(' ');
            if (dataParts.Length != 1) return null;
            if (!int.TryParse(dataParts[0], out int id)) return null;

            // Name is not part of the serialised data

            return new GameItem(id, null);
        }

        private int _id;
        private string _name;

        public int Id => _id;
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public GameItem(int id, string name)
        {
            _id = id;
            _name = name;
        }
    }
}
