using System;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Blockchain.Contracts.SimpleStorage;
using Blockchain.Contracts.SimpleStorage.ContractDefinition;
using Blockchain.Contracts.WatchContract;
using Blockchain.Contracts.WatchContract.ContractDefinition;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using server.Extensions;
using server.Models.BlockchainStructs;
using server.Models.Database;
using server.Models.Settings;
using server.Models.Validation;

namespace server.Services {
    public class ContractService {
        private readonly UserManager<UserInfo> userManager;
        private readonly ILogger<ContractService> logger;
        private readonly DefaultContext context;
        private string PrivateAccountKey = "63f6a062882ecb7945e867b8e858dfbf2b460cf86dab69b3ef7057162829dc5f";
        private string url;

        private Web3 web3 { get; set; }
        private Account account { get; set; }
        private WatchContractService watchContractService { get; set; }
        private SimpleStorageService simpleStorageService { get; set; }
        private BigInteger GUID { get; set; }

        public ContractService(ILogger<ContractService> logger, DefaultContext context, IOptions<EthereumServerSettings> settings, UserManager<UserInfo> userManager) {
            this.userManager = userManager;
            this.logger = logger;
            this.context = context;
            this.url = settings.Value.Address;
            this.account = new Account(this.PrivateAccountKey);
            this.web3 = new Web3(account, this.url);
        }

        public Watch GetWatch(string guid) {
            this.setupWatchContractCall(guid);

            // create the watch and get the parameters
            Watch returnWatch = new Watch();

            returnWatch.VendorAddress = GetVendorAddress();
            returnWatch.AssembledTimeStamp = GetAssembledTimeStamp();
            returnWatch.ClientAddress = GetClientAddress();
            returnWatch.CreationAddress = GetCreationAddress();
            returnWatch.DeliveredTimeStamp = GetDeliveredTimeStamp();
            returnWatch.DeliveryAddress = GetDeliveryAddress();
            returnWatch.GUID = GetGuid();
            returnWatch.MaterialsReceivedTimeStamp = GetMaterialsReceivedTimeStamp();
            returnWatch.SentTimeStamp = GetSentTimeStamp();
            returnWatch.ShippingAddress = GetShippingAddress();
            returnWatch.VendorAddress = GetVendorAddress();
            returnWatch.WatchType = GetWatchType();

            return returnWatch;
        }

        public ShopItem NewWatch(NewWatchForm watchForm) {
            byte[] byteArray = Guid.NewGuid().ToByteArray();
            BigInteger guid = new BigInteger(byteArray, true);

            this.setupWatchContractCall(guid.ToString());

            UserInfo vendor = this.ValidateUserWithDualAuthCode(watchForm.UserName, watchForm.DualAuthCode);

            var receiptForFunctionCall = watchContractService.CreateWatchRequestAndWaitForReceiptAsync(new CreateWatchFunction() {
                VendorAddress = vendor.BlockchainAddress,
                GUID = guid,
                WatchType = watchForm.WatchName,
                GasPrice = 0
            }).GetAwaiter().GetResult();

            ShopItem shopItem = new ShopItem() {
                Cost = watchForm.Cost,
                GUID = guid.ToString(),
                isAvailable = false,
                isSold = false,
                longDescription = watchForm.LongDescription,
                shortDescription = watchForm.ShortDescription,
                Title = watchForm.WatchName
            };
            var watchNumber = new Random().Next(1,5);
            shopItem.PictureUrl = $"/images/watch{watchNumber}.jpg";
            this.context.ShopItems.Add(shopItem);
            this.context.SaveChanges();
            return shopItem;
        }

        public Watch MaterialsReceived(string guid, BasicApiValidationForm form) {
            this.setupWatchContractCall(guid);

            UserInfo vendor = this.ValidateUserWithDualAuthCode(form.UserName, form.DualAuthCode);

            watchContractService.MaterialsReceivedRequestAsync(new MaterialsReceivedFunction() {
                VendorAddress = vendor.BlockchainAddress,
                GUID = GUID,
                GasPrice = 0
            }).GetAwaiter().GetResult();

            return GetWatch(guid);
        }

        public Watch WatchAssembled(string guid, WatchAssembledForm form) {
            this.setupWatchContractCall(guid);

            UserInfo vendor = this.ValidateUserWithDualAuthCode(form.UserName, form.DualAuthCode);

            watchContractService.WatchAssembledRequestAsync(new WatchAssembledFunction() {
                VendorAddress = vendor.BlockchainAddress,
                WatchGUID = GUID,
                PhysicalAddress = form.PhysicalAddress,
                GasPrice = 0
            }).GetAwaiter().GetResult();

            return GetWatch(guid);
        }

        public Watch WatchSent(string guid, WatchSentForm form) {
            this.setupWatchContractCall(guid);

            UserInfo vendor = this.ValidateUserWithDualAuthCode(form.UserName, form.DualAuthCode);

            watchContractService.WatchSentRequestAsync(new WatchSentFunction() {
                VendorAddress = vendor.BlockchainAddress,
                WatchGUID = GUID,
                GasPrice = 0,
                SendingAddress = form.SendingAddress,
                ReceivingAddress = form.ReceivingAddress
            }).GetAwaiter().GetResult();

            return GetWatch(guid);
        }


        public Watch WatchReceived(string guid, BasicApiValidationForm form) {
            this.setupWatchContractCall(guid);

            UserInfo vendor = this.ValidateUserWithDualAuthCode(form.UserName, form.DualAuthCode);

            watchContractService.WatchReceivedRequestAsync(new WatchReceivedFunction() {
                VendorAddress = vendor.BlockchainAddress,
                WatchGUID = GUID,
                GasPrice = 0
            }).GetAwaiter().GetResult();

            ShopItem item = this.context.ShopItems.First(s => s.GUID == guid);
            item.isAvailable = true;
            item.isSold = false;
            this.context.SaveChanges();
            return GetWatch(guid);
        }

        public BigInteger SimpleServiceGetCall() {
            this.setupSimpleStorageContractCall();
            return this.simpleStorageService.GetQueryAsync().GetAwaiter().GetResult();
        }

        public string SimpleServiceSetCall(int x) {
            this.setupSimpleStorageContractCall();
            return this.simpleStorageService.SetRequestAsync(new SetFunction() {
                X = x,
                GasPrice = 0
            }).GetAwaiter().GetResult();
        }

        private ContractDeployment GetContractDeployment<T>() where T : ContractDeploymentMessage, new() {
            T contract = new T();
            ContractDeployment deployment = null;
            WatchContractDeployment watchContractDeployment = null;
            SimpleStorageDeployment simpleStorageDeployment = null;
            if (contract is WatchContractDeployment) {
                watchContractDeployment = contract as WatchContractDeployment;
            } else if (contract is SimpleStorageDeployment) {
                simpleStorageDeployment = contract as SimpleStorageDeployment;
            }
            try {
                deployment = this.context.ContractDeployments.Where(s => s.ByteCode == contract.ByteCode && s.ServerUrl == this.url).First();
            } catch (InvalidOperationException ex) {
                logger.LogError($"contract does not currently exist: {ex}");
            }
            if (deployment == null) {
                deployment = new ContractDeployment();
                if (watchContractDeployment != null) {
                    watchContractDeployment.GasPrice = 0;
                    TransactionReceipt receipt = WatchContractService.DeployContractAndWaitForReceiptAsync(web3, watchContractDeployment).GetAwaiter().GetResult();
                    deployment.DeploymentAddress = receipt.ContractAddress;
                    deployment.ByteCode = watchContractDeployment.ByteCode;
                } else if (simpleStorageDeployment != null) {
                    simpleStorageDeployment.GasPrice = 0;
                    TransactionReceipt receipt = SimpleStorageService.DeployContractAndWaitForReceiptAsync(web3, simpleStorageDeployment).GetAwaiter().GetResult();
                    deployment.DeploymentAddress = receipt.ContractAddress;
                    deployment.ByteCode = simpleStorageDeployment.ByteCode;
                }
                deployment.ServerUrl = this.url;
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


        private DateTimeOffset GetAssembledTimeStamp() {
            BigInteger AssembledTimeStamp = watchContractService.GetAssembledTimeStampQueryAsync(new GetAssembledTimeStampFunction() { GUID = this.GUID, GasPrice = 0 }).GetAwaiter().GetResult();
            return AssembledTimeStamp.ToDateTimeOffset(); //extension function
        }

        private string GetClientAddress() {
            return watchContractService.GetClientAddressQueryAsync(new GetClientAddressFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
        }

        private string GetCreationAddress() {
            return watchContractService.GetCreationAddressQueryAsync(new GetCreationAddressFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
        }

        private DateTimeOffset GetDeliveredTimeStamp() {
            BigInteger DeliveredTimeStamp = watchContractService.GetDeliveredTimeStampQueryAsync(new GetDeliveredTimeStampFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
            return DeliveredTimeStamp.ToDateTimeOffset();
        }

        private string GetDeliveryAddress() {
            return watchContractService.GetDeliveryAddressQueryAsync(new GetDeliveryAddressFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
        }
        private BigInteger GetGuid() {
            return watchContractService.GetGUIDQueryAsync(new GetGUIDFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
        }
        private DateTimeOffset GetMaterialsReceivedTimeStamp() {
            BigInteger MaterialsReceivedTimeStamp = watchContractService.GetMaterialsReceivedTimeStampQueryAsync(new GetMaterialsReceivedTimeStampFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
            return MaterialsReceivedTimeStamp.ToDateTimeOffset();
        }
        private DateTimeOffset GetSentTimeStamp() {
            BigInteger SentTimeStamp = watchContractService.GetSentTimeStampQueryAsync(new GetSentTimeStampFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
            return SentTimeStamp.ToDateTimeOffset();
        }
        private string GetShippingAddress() {
            return watchContractService.GetShippingAddressQueryAsync(new GetShippingAddressFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
        }
        private string GetVendorAddress() {
            return watchContractService.GetVendorAddressQueryAsync(new GetVendorAddressFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
        }
        private string GetWatchType() {
            return watchContractService.GetWatchTypeQueryAsync(new GetWatchTypeFunction() { GUID = GUID, GasPrice = 0 }).GetAwaiter().GetResult();
        }

        private void setupWatchContractCall(string guid) {
            ContractDeployment deployment = GetContractDeployment<WatchContractDeployment>();

            // setup the service and common parameter
            watchContractService = new WatchContractService(web3, deployment.DeploymentAddress);
            GUID = BigInteger.Parse(guid);
        }

        private void setupSimpleStorageContractCall() {
            ContractDeployment deployment = GetContractDeployment<SimpleStorageDeployment>();
            simpleStorageService = new SimpleStorageService(web3, deployment.DeploymentAddress);
        }
    }
}