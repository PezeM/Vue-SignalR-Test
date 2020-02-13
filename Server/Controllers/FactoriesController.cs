using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Server.Hubs;
using Server.Managers;
using Server.Models;
using Server.Models.DTO;
using Server.Services;
using Server.Services.FactoryServices;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FactoriesController : ControllerBase
    {
        private readonly IHubContext<FactoryHub, IFactoryHub> _hubContext;
        private readonly ILogger<FactoriesController> _logger;
        private readonly IFactoryCreateService _factoryCreateService;
        private readonly IFactoryManager _factoryManager;
        private readonly ICraftingService _craftingService;
        
        public FactoriesController(IFactoryCreateService factoryCreateService, IFactoryManager factoryManager, IHubContext<FactoryHub, IFactoryHub> hubContext,
            ICraftingService craftingService, ILogger<FactoriesController> logger)
        {
            _logger = logger;
            _factoryCreateService = factoryCreateService;
            _factoryManager = factoryManager;
            _hubContext = hubContext;
            _craftingService = craftingService;
        }

        [HttpGet]
        public IEnumerable<FactoryInfoDto> GetAllFactories()
        {
            return _factoryManager.GetAllFactories().Select(f => new FactoryInfoDto(f));
        }

        [HttpGet("{id}")]
        public ActionResult<Factory> GetFactory(Guid id)
        {
            var factory = _factoryManager.GetFactory(id);
            _logger.LogInformation($"Found factory is {JsonSerializer.Serialize(factory)}");
            if (factory == null) return NotFound();
            
            return new JsonResult(factory);
        }

        [HttpPost("{id}/craftItem")]
        public async Task<ActionResult<Inventory>> CraftItem(Guid id, [FromBody] CraftItemRequest craftItem)
        {
            var factory = _factoryManager.GetFactory(id);
            if (factory == null) return NotFound();
            var itemToCraft = RecipesDatabase.GetRecipe(craftItem.Name);
            if (itemToCraft == null)
            {
                await _hubContext.Clients.Group(factory.Id.ToString()).SendNotification("Error",
                    $"Couldn't find item to craft named {craftItem.Name}", "danger");
                return NotFound();
            }

            if (!_craftingService.CanCraftItem(factory, itemToCraft, craftItem.Count))
            {
                await _hubContext.Clients.Group(factory.Id.ToString())
                    .SendNotification("Error", "Not enough items to make crafting", "danger");
                return NotFound();
            }

            _craftingService.AddItemToCraftQueue(factory, itemToCraft, craftItem.Count);
            await _hubContext.Clients.Group(factory.Id.ToString()).UpdateFactoryCraftingQueue(factory.Id, factory.CraftingQueue);
            return new JsonResult(factory.Inventory);
        }
    }
}