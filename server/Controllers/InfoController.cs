using System;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Blockchain.Contracts.WatchContract;
using Blockchain.Contracts.WatchContract.ContractDefinition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using server.Extensions;
using server.Models.BlockchainStructs;
using server.Models.Database;
using server.Models.Settings;
using server.Models.Validation;

namespace server.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : Controller {

        private readonly UserManager<UserInfo> userManager;
        private readonly ILogger<InfoController> logger;
        private readonly DefaultContext context;
        //private string AccountAddress = "0xF0f0E5e587287bdcd1aA4346d5774BC5315D42C6";
        private string PrivateAccountKey = "63f6a062882ecb7945e867b8e858dfbf2b460cf86dab69b3ef7057162829dc5f";
        private string url;

        public InfoController(ILogger<InfoController> logger, DefaultContext context, IOptions<EthereumServerSettings> settings, UserManager<UserInfo> userManager) {
            this.userManager = userManager;
            this.logger = logger;
            this.context = context;
            this.url = settings.Value.Address;
        }

        [HttpGet("Get/{guid}")]
        [AllowAnonymous]
        public Watch Get(string guid) {
            // setup the web3 service
            Account account = new Account(PrivateAccountKey);
            Web3 web3 = new Web3(account, url);
            ContractDeployment deployment = GetContractDeployment(web3).GetAwaiter().GetResult();

            // setup the service and common parameter
            WatchContractService service = new WatchContractService(web3, deployment.DeploymentAddress);
            BigInteger GUID = BigInteger.Parse(guid);

            // create the watch and get the parameters
            Watch returnWatch = new Watch();

            returnWatch.VendorAddress = GetVendorAddress(ref service, ref GUID);

            returnWatch.AssembledTimeStamp = getAssembledTimeStamp(ref service, ref GUID);
            returnWatch.ClientAddress = GetClientAddress(ref service, ref GUID);
            returnWatch.CreationAddress = GetCreationAddress(ref service, ref GUID);
            returnWatch.DeliveredTimeStamp = GetDeliveredTimeStamp(ref service, ref GUID);
            returnWatch.DeliveryAddress = GetDeliveryAddress(ref service, ref GUID);
            returnWatch.GUID = GetGuid(ref service, ref GUID);
            returnWatch.MaterialsReceivedTimeStamp = GetMaterialsReceivedTimeStamp(ref service, ref GUID);
            returnWatch.SentTimeStamp = GetSentTimeStamp(ref service, ref GUID);
            returnWatch.ShippingAddress = GetShippingAddress(ref service, ref GUID);
            returnWatch.VendorAddress = GetVendorAddress(ref service, ref GUID);
            returnWatch.WatchType = GetWatchType(ref service, ref GUID);

            return returnWatch;
        }

        [HttpPost("NewWatch")]
        [AllowAnonymous]
        public async Task<ShopItem> NewWatch(NewWatchForm watchForm) {
            if (ModelState.IsValid) {
                Account account = new Account(PrivateAccountKey);
                Web3 web3 = new Web3(account, url);
                ContractDeployment deployment = await GetContractDeployment(web3);
                UserInfo user = this.ValidateUserWithDualAuthCode(watchForm.UserName, watchForm.DualAuthCode);
                WatchContractService service = new WatchContractService(web3, deployment.DeploymentAddress);

                byte[] byteArray = Guid.NewGuid().ToByteArray();
                BigInteger guid = new BigInteger(byteArray, true);
                //BigInteger guid = BigInteger.Parse("114167658668439838452303663730726078317");

                var receiptForFunctionCall = await service.CreateWatchRequestAndWaitForReceiptAsync(new CreateWatchFunction() { VendorAddress = user.BlockchainAddress, GUID = guid, WatchType = "TestWatch", GasPrice = 0 });

                ShopItem shopItem = new ShopItem() {
                    Cost = watchForm.Cost,
                    GUID = guid.ToString(),
                    isAvailable = false,
                    isSold = false,
                    longDescription = watchForm.LongDescription,
                    shortDescription = watchForm.ShortDescription,
                    Title = watchForm.WatchName
                };
                this.context.ShopItems.Add(shopItem);
                this.context.ShopItems.Add(shopItem);
                this.context.SaveChanges();
                return shopItem;
            } else {
                throw new ArgumentException("invalid submission");
            }
        }

        [HttpPatch("{guid}/MaterialsReceived")]
        [AllowAnonymous]
        public Watch MaterialsReceived(string guid, BasicApiValidationForm form) {
            if (ModelState.IsValid) {
                Account account = new Account(PrivateAccountKey);
                Web3 web3 = new Web3(account, url);
                ContractDeployment deployment = GetContractDeployment(web3).GetAwaiter().GetResult();
                UserInfo user = this.ValidateUserWithDualAuthCode(form.UserName, form.DualAuthCode);
                WatchContractService service = new WatchContractService(web3, deployment.DeploymentAddress);
                BigInteger GUID = BigInteger.Parse(guid);

                service.MaterialsRecievedRequestAsync(new MaterialsRecievedFunction() { VendorAddress = user.BlockchainAddress, GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
                return Get(guid);
            }
            throw new ArgumentException("invalid submission");
        }

        [HttpPatch("{guid}/WatchAssembled")]
        [AllowAnonymous]
        public Watch WatchAssembled(string guid, WatchAssembledForm form) {
            if (ModelState.IsValid) {
                Account account = new Account(PrivateAccountKey);
                Web3 web3 = new Web3(account, url);
                ContractDeployment deployment = GetContractDeployment(web3).GetAwaiter().GetResult();
                UserInfo user = this.ValidateUserWithDualAuthCode(form.UserName, form.DualAuthCode);
                WatchContractService service = new WatchContractService(web3, deployment.DeploymentAddress);
                BigInteger GUID = BigInteger.Parse(guid);

                service.WatchAssembledRequestAsync(new WatchAssembledFunction() { VendorAddress = user.BlockchainAddress, WatchGUID = GUID, PhysicalAddress = form.PhysicalAddress, GasPrice = 0}).GetAwaiter().GetResult();
                return Get(guid);

            }
            throw new ArgumentException("invalid submission");
        }

        [HttpPatch("{guid}/WatchSent")]
        [AllowAnonymous]
        public Watch WatchSent(string guid, WatchSentForm form) {
            if (ModelState.IsValid) {
                Account account = new Account(PrivateAccountKey);
                Web3 web3 = new Web3(account, url);
                ContractDeployment deployment = GetContractDeployment(web3).GetAwaiter().GetResult();
                UserInfo user = this.ValidateUserWithDualAuthCode(form.UserName, form.DualAuthCode);
                WatchContractService service = new WatchContractService(web3, deployment.DeploymentAddress);
                BigInteger GUID = BigInteger.Parse(guid);

                service.WatchSentRequestAsync(new WatchSentFunction() { VendorAddress = user.BlockchainAddress, WatchGUID = GUID, GasPrice = 0, SendingAddress = form.SendingAddress, RecievingAddress = form.ReceivingAddress}).GetAwaiter().GetResult();
                return Get(guid);
            }

            throw new ArgumentException("invalid submission");
        }


        [HttpPatch("{guid}/WatchReceived")]
        [AllowAnonymous]
        public Watch WatchReceived(string guid, BasicApiValidationForm form) {
            if (ModelState.IsValid) {
                Account account = new Account(PrivateAccountKey);
                Web3 web3 = new Web3(account, url);
                ContractDeployment deployment = GetContractDeployment(web3).GetAwaiter().GetResult();
                UserInfo user = this.ValidateUserWithDualAuthCode(form.UserName, form.DualAuthCode);
                WatchContractService service = new WatchContractService(web3, deployment.DeploymentAddress);
                BigInteger GUID = BigInteger.Parse(guid);

                service.WatchRecievedRequestAsync(new WatchRecievedFunction() { VendorAddress = user.BlockchainAddress, WatchGUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
                
                ShopItem item = this.context.ShopItems.First(s => s.GUID == guid);
                item.isAvailable = true;
                item.isSold = false;
                this.context.SaveChanges();
                return Get(guid);
            }
            throw new ArgumentException("invalid submission");
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

        private UserInfo ValidateUserWithDualAuthCode(string userName, string DualAuthCode) {
            UserInfo user = this.context.Users.First(u => u.UserName.Equals(userName));
            bool isValid = this.userManager.VerifyTwoFactorTokenAsync(user, "GoogleAuthenticator", DualAuthCode).GetAwaiter().GetResult();
            if (isValid) {
                return user;
            } else {
                throw new ArgumentException("The Dual Authentication Code is Invalid");
            }
        }


        private DateTimeOffset getAssembledTimeStamp(ref WatchContractService service, ref BigInteger GUID) {
            BigInteger AssembledTimeStamp = service.GetAssembledTimeStampQueryAsync(new GetAssembledTimeStampFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
            return AssembledTimeStamp.ToDateTimeOffset(); //extension function
        }

        private string GetClientAddress(ref WatchContractService service, ref BigInteger GUID) {
            return service.GetClientAddressQueryAsync(new GetClientAddressFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
        }

        private string GetCreationAddress(ref WatchContractService service, ref BigInteger GUID) {
            return service.GetCreationAddressQueryAsync(new GetCreationAddressFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
        }

        private DateTimeOffset GetDeliveredTimeStamp(ref WatchContractService service, ref BigInteger GUID) {
            BigInteger DeliveredTimeStamp = service.GetDeliveredTimeStampQueryAsync(new GetDeliveredTimeStampFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
            return DeliveredTimeStamp.ToDateTimeOffset();
        }

        private string GetDeliveryAddress(ref WatchContractService service, ref BigInteger GUID) {
            return service.GetDeliveryAddressQueryAsync(new GetDeliveryAddressFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
        }
        private BigInteger GetGuid(ref WatchContractService service, ref BigInteger GUID) {
            return service.GetGUIDQueryAsync(new GetGUIDFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
        }
        private DateTimeOffset GetMaterialsReceivedTimeStamp(ref WatchContractService service, ref BigInteger GUID) {
            BigInteger MaterialsReceivedTimeStamp = service.GetMaterialsRecievedTimeStampQueryAsync(new GetMaterialsRecievedTimeStampFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
            return MaterialsReceivedTimeStamp.ToDateTimeOffset();
        }
        private DateTimeOffset GetSentTimeStamp(ref WatchContractService service, ref BigInteger GUID) {
            BigInteger SentTimeStamp = service.GetSentTimeStampQueryAsync(new GetSentTimeStampFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
            return SentTimeStamp.ToDateTimeOffset();
        }
        private string GetShippingAddress(ref WatchContractService service, ref BigInteger GUID) {
            return service.GetShippingAddressQueryAsync(new GetShippingAddressFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
        }
        private string GetVendorAddress(ref WatchContractService service, ref BigInteger GUID) {
            return service.GetVendorAddressQueryAsync(new GetVendorAddressFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
        }
        private string GetWatchType(ref WatchContractService service, ref BigInteger GUID) {
            return service.GetWatchTypeQueryAsync(new GetWatchTypeFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
        }
    }
}

//todo: finish building out API for setting the attributes and for retrieving the watch struct from the api using all of
//the get requests.