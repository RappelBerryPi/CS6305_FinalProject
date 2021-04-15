using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Models.Database;

namespace server.Controllers {
    public class ShopController : Controller {
        private readonly ILogger<ShopController> _logger;
        private readonly DefaultContext context;

        public ShopController(ILogger<ShopController> logger, DefaultContext context) {
            _logger = logger;
            this.context = context;
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
    }
}