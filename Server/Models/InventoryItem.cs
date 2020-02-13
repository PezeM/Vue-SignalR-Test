using System;
using Server.Models.Intefaces;

namespace Server.Models
{
    public class InventoryItem : IInventoryItem
    {
        public Guid Id { get; }
        public string DisplayName { get; }
        public string Name { get; }
        public int Count { get; private set; }

        public InventoryItem(IItem item, int count = 1)
        {
            Id = Guid.NewGuid();
            DisplayName = item.DisplayName;
            Name = item.Name;
            Count = count;
        }
        
        public int AddCount(int itemsToAdd)
        {
            Count += itemsToAdd;
            return Count;
        }

        public int RemoveCount(int itemsToRemove)
        {
            Count -= itemsToRemove;
            return Count;
        }
    }
}