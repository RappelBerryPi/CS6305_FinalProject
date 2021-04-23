using System.Linq;
using System.Numerics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Models.BlockchainStructs;
using server.Models.Database;
using server.Services;

namespace server.Controllers {
    public class ShopController : Controller {
        private readonly ILogger<ShopController> _logger;
        private readonly DefaultContext context;
        private ContractService ContractService { get; set; }

        public ShopController(ILogger<ShopController> logger, DefaultContext context, ContractService contractService) {
            _logger = logger;
            this.context = context;
            this.ContractService = contractService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index() {
            var ShopItems = this.context.ShopItems
                                .Where(s => s.isAvailable)
                                .Where(s => !s.isSold)
                                .OrderBy(s => s.Id).ToList();
            this.ViewData.Add("ShopItems", ShopItems);
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult View(long id) {
            var ShopItem = this.context.ShopItems
                               .Where(s => s.Id == id)
                               .FirstOrDefault();
            return View(ShopItem);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Buy(long id) {
            var ShopItem = this.context.ShopItems
                               .Where(s => s.Id == id)
                               .FirstOrDefault();
            return View(ShopItem);
        }

        [HttpGet]
        [Authorize]
        public IActionResult History(long id) {
            var ShopItem = this.context.ShopItems
                               .First(s => s.Id == id);

            Watch watch = ContractService.GetWatch(ShopItem.GUID);

            ViewData["id"] = id;
            return View(watch);
        }
    }
}