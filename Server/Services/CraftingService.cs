using Server.Managers;
using Server.Models;

namespace Server.Services
{
    public class CraftingService : ICraftingService
    {
        private readonly IFactoryManager _factoryManager;
        
        public CraftingService(IFactoryManager factoryManager)
        {
            _factoryManager = factoryManager;
        }
        
        public bool CanCraftItem(Factory factory, Recipe recipe, int itemsToCraft = 1)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                if (!factory.Inventory.TryGetItem(ingredient.Name, out var inventoryItem)) return false;
                if (inventoryItem.Count < (ingredient.Count * itemsToCraft)) return false;
            }
            
            return true;
        }

        public bool AddItemToCraftQueue(Factory factory, Recipe recipe, int itemsToCraft = 1)
        {
            if (!CanCraftItem(factory, recipe, itemsToCraft)) return false;
            foreach (var ingredient in recipe.Ingredients)
            {
                if (!factory.Inventory.RemoveItem(ingredient.Name, ingredient.Count * itemsToCraft)) return false;
            }

            factory.AddItemToCraftQueue(recipe, itemsToCraft);
            return true;
        }
    }
}