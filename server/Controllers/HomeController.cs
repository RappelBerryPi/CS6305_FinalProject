using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Models;

namespace server.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public async Task<IActionResult> Index() {
            //TODO: update and make it actually useable. deploy the contract
            var web3 = new Nethereum.Web3.Web3();
            var simpleservice = new Blockchain.Contracts.SimpleStorage.SimpleStorageService(web3, "");
            var receipt = await simpleservice.SetRequestAndWaitForReceiptAsync(new Blockchain.Contracts.SimpleStorage.ContractDefinition.SetFunction() { X = 42, Gas = 400000});
            ViewData.Add("reciept",receipt);
            return View();
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
