using System;
using System.Collections.Generic;
using Server.Services.FactoryServices;

namespace Server.Models
{
    public class Factory
    {
        public Guid Id { get; private set; }
        public string DisplayName { get; set; }
        public DateTime CreatedAt { get; private set; }
        public Inventory Inventory { get; private set; }
        
        public List<Recipe> Recipes { get; private set; }
        public List<CraftItem> CraftingQueue { get; private set; }
        public int ItemsCount => Inventory.Items.Count;
        public int RecipesCount => Recipes.Count;
        
        public Factory(Guid id, string displayName)
        {
            Id = id;
            DisplayName = displayName;
            CreatedAt = DateTime.Now;
            Inventory = new FactoryInventoryCreateService().CreateDefaultInventory(new Random().Next(100, 300));
            Recipes = RecipesDatabase.GetRandomRecipes(new Random().Next(2, 5));
            CraftingQueue = new List<CraftItem>();
        }

        public void AddRecipe(Recipe recipe)
        {
            if (Recipes.Contains(recipe)) return;
            Recipes.Add(recipe);
        }

        public void AddItemToCraftQueue(Recipe recipe, int count)
        {
            CraftingQueue.Add(new CraftItem(recipe.DisplayName, recipe.Results, recipe.MakeTime, count));
        }

        public void RemoveItemFromCraftQueue(CraftItem craftItem)
        {
            CraftingQueue.Remove(craftItem);
        }

        public void ProcessCraftingItem(CraftItem craftItem)
        {
            if(!CraftingQueue.Contains(craftItem)) return;
            foreach (var itemToMake in craftItem.ItemsToMake)
            {
                var item = ItemDatabase.Items[itemToMake.Name];
                if(item == null) continue;
                Inventory.AddNewItem(item, itemToMake.Count * craftItem.Quantity);
            }
        }
    }
}