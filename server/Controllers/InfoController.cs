using System;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Blockchain.Contracts.WatchContract;
using Blockchain.Contracts.WatchContract.ContractDefinition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using server.Models.Database;
using server.Models.Settings;

namespace server.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : Controller {

        private readonly ILogger<InfoController> logger;
        private readonly DefaultContext context;
        private string AccountAddress = "0xF0f0E5e587287bdcd1aA4346d5774BC5315D42C6";
        private string PrivateAccountKey = "63f6a062882ecb7945e867b8e858dfbf2b460cf86dab69b3ef7057162829dc5f";
        private string url;

        public InfoController(ILogger<InfoController> logger, DefaultContext context, IOptions<EthereumServerSettings> settings) {
            this.logger = logger;
            this.context = context;
            this.url = settings.Value.Address;
        }

        [HttpGet("Get/{id}")]
        [Authorize]
        public Hello Get(string id) {
            var returnObj = new Hello();
            returnObj.Id = 1;
            returnObj.Header = "Hello";
            returnObj.Message = id;
            return returnObj;
        }

        [HttpGet("test")]
        [Authorize]
        public async Task<Object> test() {
            Account account = new Account(PrivateAccountKey);
            Web3 web3 = new Web3(account, url);
            ContractDeployment deployment = await GetContractDeployment(web3);

            WatchContractService service = new WatchContractService(web3, deployment.DeploymentAddress);
            UserInfo user = this.context.Users.Where(u => u.Id == 26).First();
            byte[] byteArray = Guid.NewGuid().ToByteArray();
            //BigInteger guid = new BigInteger(byteArray, true);
            BigInteger guid = BigInteger.Parse("114167658668439838452303663730726078317");

            //var receiptForFunctionCall = await service.CreateWatchRequestAndWaitForReceiptAsync(new CreateWatchFunction() {VendorAddress = user.BlockchainAddress, GUID = guid, WatchType = "TestWatch", GasPrice = 0});
            
            var watch = await service.GetWatchTypeQueryAsync(new GetWatchTypeFunction() {GUID = guid, GasPrice = 0});
            System.Console.WriteLine(watch);
            return watch;
        }

        private async Task<ContractDeployment> GetContractDeployment(Web3 web3) {
            ContractDeployment deployment = null;
            try {
                deployment = this.context.ContractDeployments.Where(s => s.ByteCode == WatchContractDeployment.BYTECODE).First();
            } catch (InvalidOperationException ex) {
                logger.LogError($"contract does not currently exist: {ex}");
            }
            if (deployment == null) {
                WatchContractDeployment watchDeployment = new WatchContractDeployment();
                watchDeployment.GasPrice = 0;
                TransactionReceipt receipt = await WatchContractService.DeployContractAndWaitForReceiptAsync(web3, watchDeployment);

                deployment = new ContractDeployment();
                deployment.ByteCode = WatchContractDeployment.BYTECODE;
                deployment.DeploymentAddress = receipt.ContractAddress;
                deployment.ServerUrl = url;
                this.context.Add(deployment);
                this.context.SaveChanges();
            } 
            return deployment;
        }

    }
}

//todo: finish building out API for setting the attributes and for retrieving the watch struct from the api using all of
//the get requests.