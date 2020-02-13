using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Server.Helpers;
using Server.Hubs;
using Server.Models;
using Server.Services;
using Server.Services.FactoryServices;

namespace Server.Managers
{
    public class FactoryManager : IFactoryManager
    {
        private readonly ILogger<FactoryManager> _logger;
        private readonly IFactoryCreateService _factoryCreateService;
        private readonly IHubContext<FactoryHub, IFactoryHub> _hubContext;
        private readonly TimerService _timerService;
        
        public List<Factory> Factories { get; private set; }

        public FactoryManager(ILogger<FactoryManager> logger, IFactoryCreateService factoryCreateService, IHubContext<FactoryHub, IFactoryHub> hubContext)
        {
            _logger = logger;
            _factoryCreateService = factoryCreateService;
            Factories = Enumerable.Range(1, 3)
                .Select(index => _factoryCreateService.CreateNewFactory($"Fabryka {index}")).ToList();
            _logger.LogInformation($"Created {Factories.Count} factories.");
            _hubContext = hubContext;
            _timerService = new TimerService();
            _timerService.Start(1000, OnFactoryTick);
        }

        private void OnFactoryTick()
        {
            var dateNow = Time.GetTimestampMs();
            foreach (var factory in Factories)
            {
                var updatedInventory = false;
                foreach (var craftItem in factory.CraftingQueue.ToList().Where(craftItem => !(craftItem.EndAt > dateNow)))
                {
                    updatedInventory = true;
                    factory.ProcessCraftingItem(craftItem);
                    factory.CraftingQueue.Remove(craftItem);
                }

                if (updatedInventory)
                {
                    _hubContext.Clients.Group(factory.Id.ToString())
                        .UpdateFactoryCraftingQueue(factory.Id, factory.CraftingQueue);
                    _hubContext.Clients.Group(factory.Id.ToString())
                        .UpdateFactoryInventory(factory.Id, factory.Inventory);
                }
            }
        }

        public Factory GetFactory(Guid id)
        {
            return Factories.FirstOrDefault(factory => factory.Id == id);
        }

        public IEnumerable<Factory> GetAllFactories()
        {
            return Factories;
        }
    }
}