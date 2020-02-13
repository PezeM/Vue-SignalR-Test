using System;
using Server.Models.Intefaces;

namespace Server.Models
{
    public class Item : IItem
    {
        public string DisplayName { get; }
        public string Name { get; }

        public Item(string displayName, string name)
        {
            DisplayName = displayName;
            Name = name;
        }
    }
}