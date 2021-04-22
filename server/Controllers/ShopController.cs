using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Models.BlockchainStructs;
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
        public async Task<IActionResult> History(long id) {
            var ShopItem = this.context.ShopItems
                               .First(s => s.Id == id);

            string hostURL = string.Format("{0}://{1}:{2}/",Request.Scheme, Request.Host.Host, Request.Host.Port);

            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri(hostURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            Watch watch = null;
            HttpResponseMessage message = await client.GetAsync("api/Info/Get/" + ShopItem.GUID);
            if (message.IsSuccessStatusCode) {
                var contentStream = await message.Content.ReadAsStreamAsync();
                try {
                    watch = await System.Text.Json.JsonSerializer.DeserializeAsync<Watch>(contentStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});
                    watch.GUID = BigInteger.Parse(ShopItem.GUID);
                } catch (JsonException ex) {
                    this._logger.LogCritical(new EventId(-1, "Json Exception"), ex, "", null);
                }
            }
            ViewData["id"] = id;
            return View(watch);
        }
    }
    // TODO: Finish Creating the Buy INterface. create actual pictures and link them back into the project. Take a few
    // screenshots and show drew. Create the Contract for the milk and deploy all of these contracts to the production
    // azure service and then check all of the URLS everywhere to make sure that they're configurable in the appsettings.
}