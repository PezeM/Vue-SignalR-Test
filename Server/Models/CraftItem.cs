using System;
using System.Collections.Generic;
using Server.Helpers;

namespace Server.Models
{
    public class CraftItem
    {
        public Guid Id { get; private set; }
        public double CreatedAt { get; private set; }
        public double EndAt { get; private set; }
        public string ItemDisplayName { get; private set; }
        public List<RecipeItem> ItemsToMake { get; private set; }
        public int Quantity { get; private set; }

        public CraftItem(string displayName, List<RecipeItem> itemsToMake, int makeTime, int quantity = 1)
        {
            Id = Guid.NewGuid();
            CreatedAt = Time.GetTimestampMs();
            EndAt = Time.GetTimestampMs() + makeTime;
            ItemDisplayName = displayName;
            ItemsToMake = itemsToMake;
            Quantity = quantity;
        }
    }
}