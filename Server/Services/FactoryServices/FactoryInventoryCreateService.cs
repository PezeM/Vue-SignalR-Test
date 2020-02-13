using System;
using System.Linq;
using Server.Models;

namespace Server.Services.FactoryServices
{
    public class FactoryInventoryCreateService : IFactoryInventoryCreateService
    {
        private Random _rng;

        public FactoryInventoryCreateService()
        {
            _rng = new Random();
        }
        
        public Inventory CreateDefaultInventory(int maximumItems = 50)
        {
            var inventory = new Inventory();
            for (var i = 0; i < maximumItems; i++)
            {
                var item = ItemDatabase.Items.ElementAt(_rng.Next(0, ItemDatabase.Items.Count)).Value;
                inventory.AddNewItem(item);
            }
            return inventory;
        }
    }
}