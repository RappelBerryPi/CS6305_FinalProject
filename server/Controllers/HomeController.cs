using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Blockchain.Contracts.SimpleStorage;
using Blockchain.Contracts.SimpleStorage.ContractDefinition;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Signer;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using server.Models;
using server.Models.Database;
using server.Models.Settings;

namespace server.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> logger;
        private readonly DefaultContext context;
        private string AccountAddress = "0xF0f0E5e587287bdcd1aA4346d5774BC5315D42C6";
        private string PrivateAccountKey = "63f6a062882ecb7945e867b8e858dfbf2b460cf86dab69b3ef7057162829dc5f";
        private string url;

        public HomeController(ILogger<HomeController> logger, DefaultContext context, IOptions<EthereumServerSettings> settings) {
            this.logger = logger;
            this.context = context;
            this.url = settings.Value.Address;
        }

        public async Task<IActionResult> Index() {
            Account account = new Account(PrivateAccountKey);
            Web3 web3 = new Web3(account, url);

            ContractDeployment deployment = await GetContractDeployment(web3);

            SimpleStorageService simpleService = new SimpleStorageService(web3, deployment.DeploymentAddress);
            
            BigInteger intValue = await simpleService.GetQueryAsync();

            var Hellos = this.context.Hellos.OrderBy(H => H.Id).ToList();
            this.ViewData.Add("Hellos", Hellos);
            this.ViewData.Add("intValue", intValue);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Hello hello) {
            this.context.Add(hello);
            this.context.SaveChanges();

            var account = new Account(PrivateAccountKey);
            var web3 = new Web3(account, url);

            ContractDeployment deployment = await GetContractDeployment(web3);

            var service = new SimpleStorageService(web3, deployment.DeploymentAddress);
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

        private async Task<ContractDeployment> GetContractDeployment(Web3 web3) {
            ContractDeployment deployment = this.context.ContractDeployments.Where(s => s.ByteCode == SimpleStorageDeployment.BYTECODE).First();
            if (deployment == null) {
                SimpleStorageDeployment simpleDeployment = new SimpleStorageDeployment();
                simpleDeployment.GasPrice = 0;
                TransactionReceipt receipt = await SimpleStorageService.DeployContractAndWaitForReceiptAsync(web3, simpleDeployment);

                deployment = new ContractDeployment();
                deployment.ByteCode = SimpleStorageDeployment.BYTECODE;
                deployment.DeploymentAddress = receipt.ContractAddress;
                deployment.ServerUrl = url;
                this.context.Add(deployment);
                this.context.SaveChanges();
            } 
            return deployment;

        }
    }
}