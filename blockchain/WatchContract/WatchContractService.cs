using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Blockchain.Contracts.WatchContract.ContractDefinition;

namespace Blockchain.Contracts.WatchContract
{
    public partial class WatchContractService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, WatchContractDeployment watchContractDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<WatchContractDeployment>().SendRequestAndWaitForReceiptAsync(watchContractDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, WatchContractDeployment watchContractDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<WatchContractDeployment>().SendRequestAsync(watchContractDeployment);
        }

        public static async Task<WatchContractService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, WatchContractDeployment watchContractDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, watchContractDeployment, cancellationTokenSource);
            return new WatchContractService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public WatchContractService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<BigInteger> GetAssembledTimeStampQueryAsync(GetAssembledTimeStampFunction getAssembledTimeStampFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetAssembledTimeStampFunction, BigInteger>(getAssembledTimeStampFunction, blockParameter);
        }

        
        public Task<BigInteger> GetAssembledTimeStampQueryAsync(BigInteger gUID, BlockParameter blockParameter = null)
        {
            var getAssembledTimeStampFunction = new GetAssembledTimeStampFunction();
                getAssembledTimeStampFunction.GUID = gUID;
            
            return ContractHandler.QueryAsync<GetAssembledTimeStampFunction, BigInteger>(getAssembledTimeStampFunction, blockParameter);
        }

        public Task<string> GetClientAddressQueryAsync(GetClientAddressFunction getClientAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetClientAddressFunction, string>(getClientAddressFunction, blockParameter);
        }

        
        public Task<string> GetClientAddressQueryAsync(BigInteger gUID, BlockParameter blockParameter = null)
        {
            var getClientAddressFunction = new GetClientAddressFunction();
                getClientAddressFunction.GUID = gUID;
            
            return ContractHandler.QueryAsync<GetClientAddressFunction, string>(getClientAddressFunction, blockParameter);
        }

        public Task<string> GetCreationAddressQueryAsync(GetCreationAddressFunction getCreationAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetCreationAddressFunction, string>(getCreationAddressFunction, blockParameter);
        }

        
        public Task<string> GetCreationAddressQueryAsync(BigInteger gUID, BlockParameter blockParameter = null)
        {
            var getCreationAddressFunction = new GetCreationAddressFunction();
                getCreationAddressFunction.GUID = gUID;
            
            return ContractHandler.QueryAsync<GetCreationAddressFunction, string>(getCreationAddressFunction, blockParameter);
        }

        public Task<BigInteger> GetDeliveredTimeStampQueryAsync(GetDeliveredTimeStampFunction getDeliveredTimeStampFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetDeliveredTimeStampFunction, BigInteger>(getDeliveredTimeStampFunction, blockParameter);
        }

        
        public Task<BigInteger> GetDeliveredTimeStampQueryAsync(BigInteger gUID, BlockParameter blockParameter = null)
        {
            var getDeliveredTimeStampFunction = new GetDeliveredTimeStampFunction();
                getDeliveredTimeStampFunction.GUID = gUID;
            
            return ContractHandler.QueryAsync<GetDeliveredTimeStampFunction, BigInteger>(getDeliveredTimeStampFunction, blockParameter);
        }

        public Task<string> GetDeliveryAddressQueryAsync(GetDeliveryAddressFunction getDeliveryAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetDeliveryAddressFunction, string>(getDeliveryAddressFunction, blockParameter);
        }

        
        public Task<string> GetDeliveryAddressQueryAsync(BigInteger gUID, BlockParameter blockParameter = null)
        {
            var getDeliveryAddressFunction = new GetDeliveryAddressFunction();
                getDeliveryAddressFunction.GUID = gUID;
            
            return ContractHandler.QueryAsync<GetDeliveryAddressFunction, string>(getDeliveryAddressFunction, blockParameter);
        }

        public Task<BigInteger> GetGUIDQueryAsync(GetGUIDFunction getGUIDFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetGUIDFunction, BigInteger>(getGUIDFunction, blockParameter);
        }

        
        public Task<BigInteger> GetGUIDQueryAsync(BigInteger gUID, BlockParameter blockParameter = null)
        {
            var getGUIDFunction = new GetGUIDFunction();
                getGUIDFunction.GUID = gUID;
            
            return ContractHandler.QueryAsync<GetGUIDFunction, BigInteger>(getGUIDFunction, blockParameter);
        }

        public Task<BigInteger> GetMaterialsRecievedTimeStampQueryAsync(GetMaterialsRecievedTimeStampFunction getMaterialsRecievedTimeStampFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetMaterialsRecievedTimeStampFunction, BigInteger>(getMaterialsRecievedTimeStampFunction, blockParameter);
        }

        
        public Task<BigInteger> GetMaterialsRecievedTimeStampQueryAsync(BigInteger gUID, BlockParameter blockParameter = null)
        {
            var getMaterialsRecievedTimeStampFunction = new GetMaterialsRecievedTimeStampFunction();
                getMaterialsRecievedTimeStampFunction.GUID = gUID;
            
            return ContractHandler.QueryAsync<GetMaterialsRecievedTimeStampFunction, BigInteger>(getMaterialsRecievedTimeStampFunction, blockParameter);
        }

        public Task<BigInteger> GetSentTimeStampQueryAsync(GetSentTimeStampFunction getSentTimeStampFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetSentTimeStampFunction, BigInteger>(getSentTimeStampFunction, blockParameter);
        }

        
        public Task<BigInteger> GetSentTimeStampQueryAsync(BigInteger gUID, BlockParameter blockParameter = null)
        {
            var getSentTimeStampFunction = new GetSentTimeStampFunction();
                getSentTimeStampFunction.GUID = gUID;
            
            return ContractHandler.QueryAsync<GetSentTimeStampFunction, BigInteger>(getSentTimeStampFunction, blockParameter);
        }

        public Task<string> GetShippingAddressQueryAsync(GetShippingAddressFunction getShippingAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetShippingAddressFunction, string>(getShippingAddressFunction, blockParameter);
        }

        
        public Task<string> GetShippingAddressQueryAsync(BigInteger gUID, BlockParameter blockParameter = null)
        {
            var getShippingAddressFunction = new GetShippingAddressFunction();
                getShippingAddressFunction.GUID = gUID;
            
            return ContractHandler.QueryAsync<GetShippingAddressFunction, string>(getShippingAddressFunction, blockParameter);
        }

        public Task<string> GetVendorAddressQueryAsync(GetVendorAddressFunction getVendorAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetVendorAddressFunction, string>(getVendorAddressFunction, blockParameter);
        }

        
        public Task<string> GetVendorAddressQueryAsync(BigInteger gUID, BlockParameter blockParameter = null)
        {
            var getVendorAddressFunction = new GetVendorAddressFunction();
                getVendorAddressFunction.GUID = gUID;
            
            return ContractHandler.QueryAsync<GetVendorAddressFunction, string>(getVendorAddressFunction, blockParameter);
        }

        public Task<string> GetWatchTypeQueryAsync(GetWatchTypeFunction getWatchTypeFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetWatchTypeFunction, string>(getWatchTypeFunction, blockParameter);
        }

        
        public Task<string> GetWatchTypeQueryAsync(BigInteger gUID, BlockParameter blockParameter = null)
        {
            var getWatchTypeFunction = new GetWatchTypeFunction();
                getWatchTypeFunction.GUID = gUID;
            
            return ContractHandler.QueryAsync<GetWatchTypeFunction, string>(getWatchTypeFunction, blockParameter);
        }

        public Task<bool> StringsAreEqualQueryAsync(StringsAreEqualFunction stringsAreEqualFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<StringsAreEqualFunction, bool>(stringsAreEqualFunction, blockParameter);
        }

        
        public Task<bool> StringsAreEqualQueryAsync(string str, string other, BlockParameter blockParameter = null)
        {
            var stringsAreEqualFunction = new StringsAreEqualFunction();
                stringsAreEqualFunction.Str = str;
                stringsAreEqualFunction.Other = other;
            
            return ContractHandler.QueryAsync<StringsAreEqualFunction, bool>(stringsAreEqualFunction, blockParameter);
        }

        public Task<string> WatchAssembledRequestAsync(WatchAssembledFunction watchAssembledFunction)
        {
             return ContractHandler.SendRequestAsync(watchAssembledFunction);
        }

        public Task<TransactionReceipt> WatchAssembledRequestAndWaitForReceiptAsync(WatchAssembledFunction watchAssembledFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(watchAssembledFunction, cancellationToken);
        }

        public Task<string> WatchAssembledRequestAsync(string vendorAddress, string physicalAddress, BigInteger watchGUID)
        {
            var watchAssembledFunction = new WatchAssembledFunction();
                watchAssembledFunction.VendorAddress = vendorAddress;
                watchAssembledFunction.PhysicalAddress = physicalAddress;
                watchAssembledFunction.WatchGUID = watchGUID;
            
             return ContractHandler.SendRequestAsync(watchAssembledFunction);
        }

        public Task<TransactionReceipt> WatchAssembledRequestAndWaitForReceiptAsync(string vendorAddress, string physicalAddress, BigInteger watchGUID, CancellationTokenSource cancellationToken = null)
        {
            var watchAssembledFunction = new WatchAssembledFunction();
                watchAssembledFunction.VendorAddress = vendorAddress;
                watchAssembledFunction.PhysicalAddress = physicalAddress;
                watchAssembledFunction.WatchGUID = watchGUID;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(watchAssembledFunction, cancellationToken);
        }

        public Task<string> WatchRecievedRequestAsync(WatchRecievedFunction watchRecievedFunction)
        {
             return ContractHandler.SendRequestAsync(watchRecievedFunction);
        }

        public Task<TransactionReceipt> WatchRecievedRequestAndWaitForReceiptAsync(WatchRecievedFunction watchRecievedFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(watchRecievedFunction, cancellationToken);
        }

        public Task<string> WatchRecievedRequestAsync(string vendorAddress, BigInteger watchGUID)
        {
            var watchRecievedFunction = new WatchRecievedFunction();
                watchRecievedFunction.VendorAddress = vendorAddress;
                watchRecievedFunction.WatchGUID = watchGUID;
            
             return ContractHandler.SendRequestAsync(watchRecievedFunction);
        }

        public Task<TransactionReceipt> WatchRecievedRequestAndWaitForReceiptAsync(string vendorAddress, BigInteger watchGUID, CancellationTokenSource cancellationToken = null)
        {
            var watchRecievedFunction = new WatchRecievedFunction();
                watchRecievedFunction.VendorAddress = vendorAddress;
                watchRecievedFunction.WatchGUID = watchGUID;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(watchRecievedFunction, cancellationToken);
        }

        public Task<string> WatchSentRequestAsync(WatchSentFunction watchSentFunction)
        {
             return ContractHandler.SendRequestAsync(watchSentFunction);
        }

        public Task<TransactionReceipt> WatchSentRequestAndWaitForReceiptAsync(WatchSentFunction watchSentFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(watchSentFunction, cancellationToken);
        }

        public Task<string> WatchSentRequestAsync(string vendorAddress, BigInteger watchGUID, string sendingAddress, string recievingAddress)
        {
            var watchSentFunction = new WatchSentFunction();
                watchSentFunction.VendorAddress = vendorAddress;
                watchSentFunction.WatchGUID = watchGUID;
                watchSentFunction.SendingAddress = sendingAddress;
                watchSentFunction.RecievingAddress = recievingAddress;
            
             return ContractHandler.SendRequestAsync(watchSentFunction);
        }

        public Task<TransactionReceipt> WatchSentRequestAndWaitForReceiptAsync(string vendorAddress, BigInteger watchGUID, string sendingAddress, string recievingAddress, CancellationTokenSource cancellationToken = null)
        {
            var watchSentFunction = new WatchSentFunction();
                watchSentFunction.VendorAddress = vendorAddress;
                watchSentFunction.WatchGUID = watchGUID;
                watchSentFunction.SendingAddress = sendingAddress;
                watchSentFunction.RecievingAddress = recievingAddress;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(watchSentFunction, cancellationToken);
        }

        public Task<string> CreateWatchRequestAsync(CreateWatchFunction createWatchFunction)
        {
             return ContractHandler.SendRequestAsync(createWatchFunction);
        }

        public Task<TransactionReceipt> CreateWatchRequestAndWaitForReceiptAsync(CreateWatchFunction createWatchFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createWatchFunction, cancellationToken);
        }

        public Task<string> CreateWatchRequestAsync(string vendorAddress, BigInteger gUID, string watchType)
        {
            var createWatchFunction = new CreateWatchFunction();
                createWatchFunction.VendorAddress = vendorAddress;
                createWatchFunction.GUID = gUID;
                createWatchFunction.WatchType = watchType;
            
             return ContractHandler.SendRequestAsync(createWatchFunction);
        }

        public Task<TransactionReceipt> CreateWatchRequestAndWaitForReceiptAsync(string vendorAddress, BigInteger gUID, string watchType, CancellationTokenSource cancellationToken = null)
        {
            var createWatchFunction = new CreateWatchFunction();
                createWatchFunction.VendorAddress = vendorAddress;
                createWatchFunction.GUID = gUID;
                createWatchFunction.WatchType = watchType;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(createWatchFunction, cancellationToken);
        }

        public Task<string> MaterialsRecievedRequestAsync(MaterialsRecievedFunction materialsRecievedFunction)
        {
             return ContractHandler.SendRequestAsync(materialsRecievedFunction);
        }

        public Task<TransactionReceipt> MaterialsRecievedRequestAndWaitForReceiptAsync(MaterialsRecievedFunction materialsRecievedFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(materialsRecievedFunction, cancellationToken);
        }

        public Task<string> MaterialsRecievedRequestAsync(string vendorAddress, BigInteger gUID)
        {
            var materialsRecievedFunction = new MaterialsRecievedFunction();
                materialsRecievedFunction.VendorAddress = vendorAddress;
                materialsRecievedFunction.GUID = gUID;
            
             return ContractHandler.SendRequestAsync(materialsRecievedFunction);
        }

        public Task<TransactionReceipt> MaterialsRecievedRequestAndWaitForReceiptAsync(string vendorAddress, BigInteger gUID, CancellationTokenSource cancellationToken = null)
        {
            var materialsRecievedFunction = new MaterialsRecievedFunction();
                materialsRecievedFunction.VendorAddress = vendorAddress;
                materialsRecievedFunction.GUID = gUID;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(materialsRecievedFunction, cancellationToken);
        }
    }
}
