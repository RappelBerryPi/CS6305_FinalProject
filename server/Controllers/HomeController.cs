using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Blockchain.Contracts.SimpleStorage;
using Blockchain.Contracts.SimpleStorage.ContractDefinition;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Models;
using server.Models.Database;

namespace server.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly DefaultContext context;

        public HomeController(ILogger<HomeController> logger, DefaultContext context) {
            _logger = logger;
            this.context = context;
        }

        public async Task<IActionResult> Index() {
            var account = new Nethereum.Web3.Accounts.Account("0x2b660bdec9aa543b8786e6ab0ec16c2947a4c8f443493fdf344f9660e4c23311");
            /*
            var web3 = new Nethereum.Web3.Web3(account, "http://127.0.0.1:8545/");
            //var deployment = new SimpleStorageDeployment();
            //deployment.GasPrice = 0;
            //var receipt = await SimpleStorageService.DeployContractAndWaitForReceiptAsync(web3, deployment);
            var ContractAddress = "0x326dbf73379982e28d09f91e50ba2b12eee70655";
            var service = new SimpleStorageService(web3, ContractAddress);
            //var accounts = await web3.Personal.ListAccounts.SendRequestAsync();
            //var simpleService = new SimpleStorageService(web3, "0x7ff32c8fde52dd2cf4584d7f56c5e75a9c63d663");
            //var receiptForSetFunctionCall = await service.SetRequestAndWaitForReceiptAsync(new SetFunction() { X = 42, GasPrice = 0});
            var intvalue = await service.GetQueryAsync();
            /* the below two lines do the same thing */
            var intvalue = new Random().Next();
            var Hellos = this.context.Hellos.OrderBy(H => H.Id).ToList();


            this.ViewData.Add("Hellos", Hellos);
            this.ViewData.Add("intValue", (System.Nullable<BigInteger>) intvalue);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Hello hello) {
            this.context.Add(hello);
            this.context.SaveChanges();
            System.Console.WriteLine(hello);
            var account = new Nethereum.Web3.Accounts.Account("0x2b660bdec9aa543b8786e6ab0ec16c2947a4c8f443493fdf344f9660e4c23311");
            var web3 = new Nethereum.Web3.Web3(account, "http://127.0.0.1:8545/");
            var ContractAddress = "0x326dbf73379982e28d09f91e50ba2b12eee70655";
            var service = new SimpleStorageService(web3, ContractAddress);
            var x = new System.Random().Next();
            var receiptForSetFunctionCall = await service.SetRequestAndWaitForReceiptAsync(new SetFunction() { X = x, GasPrice = 0 });
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