using System.Collections.Generic;
using System.Linq;
using Server.Models.Intefaces;

namespace Server.Models
{
    public class Inventory
    {
        public IReadOnlyCollection<IInventoryItem> Items => _items;
        private List<IInventoryItem> _items;

        public Inventory()
        {
            _items = new List<IInventoryItem>();
        }

        public IInventoryItem AddNewItem(IItem item, int count = 1)
        {
            if (TryGetItem(item.Name, out var inventoryItem))
            {
                inventoryItem.AddCount(count);
                return inventoryItem;
            }

            var newInventoryItem = new InventoryItem(item, count);
            _items.Add(newInventoryItem);
            return newInventoryItem;
        }
        
        public void AddNewItem(IInventoryItem item)
        {
            if (TryGetItem(item.Name, out var existingItem))
            {
                existingItem.AddCount(item.Count);
                return;
            }

            _items.Add(item);
        }

        public bool HasItem(string itemName) => _items.Any(item => item.Name == itemName);

        public bool HasItem(IItem item) => HasItem(item.Name);
        
        public bool TryGetItem(string itemName, out IInventoryItem item)
        {
            item = null;
            foreach (var currentItem in _items)
            {
                if (currentItem.Name != itemName) continue;
                item = currentItem;
                return true;
            }

            return item != null;
        }

        public bool RemoveItem(string itemName, int count)
        {
            if (!TryGetItem(itemName, out var inventoryItem)) return false;
            inventoryItem.RemoveCount(count);
            if (inventoryItem.Count <= 0)
            {
                _items.Remove(inventoryItem);
            }
            return true;
        }
    }
}