using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Server.Models;

namespace Server.Hubs
{
    public interface IFactoryHub
    {
        Task ReceiveMessage(string message);
        Task SendNotification(string title, string message, string notificationType);
        Task UpdateFactoryCraftingQueue(Guid factoryId, List<CraftItem> craftingQueue);
        Task UpdateFactoryInventory(Guid factoryId, Inventory factoryInventory);
    }
    
    public class FactoryHub : Hub<IFactoryHub>
    {
        public async Task SendMessageToAllClients()
        {
            await Clients.All.ReceiveMessage("Eluwa co");
        }

        public async Task JoinFactoryPage(Guid factoryId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, factoryId.ToString());
        }

        public async Task LeaveFactoryPage(Guid factoryId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, factoryId.ToString());
        }
    }
}