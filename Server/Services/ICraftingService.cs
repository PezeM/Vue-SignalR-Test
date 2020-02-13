using Server.Models;

namespace Server.Services
{
    public interface ICraftingService
    {
        bool CanCraftItem(Factory factory, Recipe recipe, int itemsToCraft = 1);
        bool AddItemToCraftQueue(Factory factory, Recipe recipe, int itemsToCraft = 1);
    }
}