using Server.Models;

namespace Server.Services.FactoryServices
{
    public interface IFactoryInventoryCreateService
    {
        Inventory CreateDefaultInventory(int maximumItems = 50);
    }
}