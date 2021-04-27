using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Models;
using server.Models.Database;
using server.Services;

namespace server.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> logger;
        private readonly DefaultContext context;
        private ContractService ContractService;

        public HomeController(ILogger<HomeController> logger, DefaultContext context, ContractService contractService) {
            this.logger = logger;
            this.context = context;
            this.ContractService = contractService;
        }

        public async Task<IActionResult> Index() {
            BigInteger intValue = await ContractService.SimpleServiceGetCall();
            var Hellos = this.context.Hellos.OrderBy(H => H.Id).ToList();
            this.ViewData.Add("Hellos", Hellos);
            this.ViewData.Add("intValue", intValue);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexPost() {
            await this.ContractService.SimpleServiceSetCall(new System.Random().Next());
            return RedirectToAction("Index");
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}