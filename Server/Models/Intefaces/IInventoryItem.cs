using System;

namespace Server.Models.Intefaces
{
    public interface IInventoryItem
    {
        Guid Id { get; }
        string DisplayName { get; }
        string Name { get; }
        int Count { get; }
        int AddCount(int itemsToAdd);
        int RemoveCount(int itemsToRemove);
    }
}