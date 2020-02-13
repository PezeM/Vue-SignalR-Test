using Server.Models.Intefaces;

namespace Server.Models
{
    public class RecipeItem
    {
        public string DisplayName { get; }
        public string Name { get; }
        public int Count { get; }

        public RecipeItem(IItem baseItem, int count = 1)
        {
            DisplayName = baseItem.DisplayName;
            Name = baseItem.Name;
            Count = count;
        }
    }
}