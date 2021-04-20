using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace Blockchain.Contracts.WatchContract.ContractDefinition
{


    public partial class WatchContractDeployment : WatchContractDeploymentBase
    {
        public WatchContractDeployment() : base(BYTECODE) { }
        public WatchContractDeployment(string byteCode) : base(byteCode) { }
    }

    public class WatchContractDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b50610fd9806100206000396000f3fe608060405234801561001057600080fd5b506004361061010b5760003560e01c80638f174f30116100a2578063d02abe4111610071578063d02abe41146102b1578063dd448c65146102c4578063f2f14c41146102d7578063f58e7524146102ea578063f7ab33e6146103315761010b565b80638f174f301461024c5780639b7cda071461025f578063b3b4da1314610272578063bb259e42146102855761010b565b80637e05646a116100de5780637e05646a146101a55780637ecd4fd2146101b857806383739b66146102055780638bed21df146102375761010b565b80631910668d146101105780633728eca114610136578063519e4db81461015657806357c8640114610182575b600080fd5b61012361011e366004610ebd565b61035d565b6040519081526020015b60405180910390f35b610149610144366004610ebd565b61037f565b60405161012d9190610ef3565b610123610164366004610ebd565b6001600160801b03166000908152602081905260409020600a015490565b610195610190366004610e5c565b61042e565b604051901515815260200161012d565b6101496101b3366004610ebd565b610487565b6101ed6101c6366004610ebd565b6001600160801b03166000908152602081905260409020600101546001600160a01b031690565b6040516001600160a01b03909116815260200161012d565b6101ed610213366004610ebd565b6001600160801b03166000908152602081905260409020546001600160a01b031690565b61024a610245366004610d4c565b6104b1565b005b61014961025a366004610ebd565b6105e4565b61024a61026d366004610cf0565b61060e565b61024a610280366004610d7e565b610759565b610123610293366004610ebd565b6001600160801b031660009081526020819052604090206008015490565b6101496102bf366004610ebd565b6107f3565b61024a6102d2366004610dda565b61081d565b61024a6102e5366004610d4c565b610a1d565b6103196102f8366004610ebd565b6001600160801b039081166000908152602081905260409020600201541690565b6040516001600160801b03909116815260200161012d565b61012361033f366004610ebd565b6001600160801b031660009081526020819052604090206003015490565b6001600160801b0381166000908152602081905260409020600901545b919050565b6001600160801b03811660009081526020819052604090206005018054606091906103a990610f52565b80601f01602080910402602001604051908101604052809291908181526020018280546103d590610f52565b80156104225780601f106103f757610100808354040283529160200191610422565b820191906000526020600020905b81548152906001019060200180831161040557829003601f168201915b50505050509050919050565b6000816040516020016104419190610ed7565b60405160208183030381529060405280519060200120836040516020016104689190610ed7565b6040516020818303038152906040528051906020012014905092915050565b6001600160801b03811660009081526020819052604090206004018054606091906103a990610f52565b60006104bd8383610ac1565b42600a820155600280820180546001600160801b039081166000908152602081905260409020845481546001600160a01b03199081166001600160a01b0392831617835560018088015490840180549092169216919091179055915492820180546001600160801b0319169390911692909217909155600380830154908201556004808301805493945084939183019161055690610f52565b610561929190610b27565b50600582018160050190805461057690610f52565b610581929190610b27565b50600682018160060190805461059690610f52565b6105a1929190610b27565b5060078201816007019080546105b690610f52565b6105c1929190610b27565b506008828101549082015560098083015490820155600a91820154910155505050565b6001600160801b03811660009081526020819052604090206007018054606091906103a990610f52565b600061061a8483610ac1565b42600882015583519091506106389060058301906020860190610bb2565b50600280820180546001600160801b039081166000908152602081905260409020845481546001600160a01b03199081166001600160a01b0392831617835560018088015490840180549092169216919091179055915492820180546001600160801b03191693909116929092179091556003808301549082015560048083018054849392830191906106ca90610f52565b6106d5929190610b27565b5060058201816005019080546106ea90610f52565b6106f5929190610b27565b50600682018160060190805461070a90610f52565b610715929190610b27565b50600782018160070190805461072a90610f52565b610735929190610b27565b506008828101549082015560098083015490820155600a9182015491015550505050565b6001600160801b03808316600081815260208190526040902060020154909116141561078457600080fd5b6001600160801b03821660008181526020818152604090912080546001600160a01b0387166001600160a01b0319918216178255600182018054909116331790556002810180546001600160801b03191690931790925582516107ed9260040191840190610bb2565b50505050565b6001600160801b03811660009081526020819052604090206006018054606091906103a990610f52565b60006108298585610ac1565b90506108c181600501805461083d90610f52565b80601f016020809104026020016040519081016040528092919081815260200182805461086990610f52565b80156108b65780601f1061088b576101008083540402835291602001916108b6565b820191906000526020600020905b81548152906001019060200180831161089957829003601f168201915b50505050508461042e565b6108ca57600080fd5b42600982015582516108e59060068301906020860190610bb2565b5081516108fb9060078301906020850190610bb2565b50600280820180546001600160801b039081166000908152602081905260409020845481546001600160a01b03199081166001600160a01b0392831617835560018088015490840180549092169216919091179055915492820180546001600160801b031916939091169290921790915560038083015490820155600480830180548493928301919061098d90610f52565b610998929190610b27565b5060058201816005019080546109ad90610f52565b6109b8929190610b27565b5060068201816006019080546109cd90610f52565b6109d8929190610b27565b5060078201816007019080546109ed90610f52565b6109f8929190610b27565b506008828101549082015560098083015490820155600a918201549101555050505050565b6000610a298383610ac1565b426003808301918255600280840180546001600160801b039081166000908152602081905260409020865481546001600160a01b03199081166001600160a01b039283161783556001808a015490840180549092169216919091179055915492820180546001600160801b03191693909116929092179091559154908201556004808301805493945084939183019161055690610f52565b60006001600160801b038216610ad657600080fd5b6001600160801b038216600090815260208190526040902080546001600160a01b03858116911614610b0757600080fd5b60018101546001600160a01b03163314610b2057600080fd5b9392505050565b828054610b3390610f52565b90600052602060002090601f016020900481019282610b555760008555610ba2565b82601f10610b665780548555610ba2565b82800160010185558215610ba257600052602060002091601f016020900482015b82811115610ba2578254825591600101919060010190610b87565b50610bae929150610c26565b5090565b828054610bbe90610f52565b90600052602060002090601f016020900481019282610be05760008555610ba2565b82601f10610bf957805160ff1916838001178555610ba2565b82800160010185558215610ba2579182015b82811115610ba2578251825591602001919060010190610c0b565b5b80821115610bae5760008155600101610c27565b80356001600160a01b038116811461037a57600080fd5b600082601f830112610c62578081fd5b813567ffffffffffffffff80821115610c7d57610c7d610f8d565b604051601f8301601f19908116603f01168101908282118183101715610ca557610ca5610f8d565b81604052838152866020858801011115610cbd578485fd5b8360208701602083013792830160200193909352509392505050565b80356001600160801b038116811461037a57600080fd5b600080600060608486031215610d04578283fd5b610d0d84610c3b565b9250602084013567ffffffffffffffff811115610d28578283fd5b610d3486828701610c52565b925050610d4360408501610cd9565b90509250925092565b60008060408385031215610d5e578182fd5b610d6783610c3b565b9150610d7560208401610cd9565b90509250929050565b600080600060608486031215610d92578283fd5b610d9b84610c3b565b9250610da960208501610cd9565b9150604084013567ffffffffffffffff811115610dc4578182fd5b610dd086828701610c52565b9150509250925092565b60008060008060808587031215610def578081fd5b610df885610c3b565b9350610e0660208601610cd9565b9250604085013567ffffffffffffffff80821115610e22578283fd5b610e2e88838901610c52565b93506060870135915080821115610e43578283fd5b50610e5087828801610c52565b91505092959194509250565b60008060408385031215610e6e578182fd5b823567ffffffffffffffff80821115610e85578384fd5b610e9186838701610c52565b93506020850135915080821115610ea6578283fd5b50610eb385828601610c52565b9150509250929050565b600060208284031215610ece578081fd5b610b2082610cd9565b60008251610ee9818460208701610f26565b9190910192915050565b6000602082528251806020840152610f12816040850160208701610f26565b601f01601f19169190910160400192915050565b60005b83811015610f41578181015183820152602001610f29565b838111156107ed5750506000910152565b600181811c90821680610f6657607f821691505b60208210811415610f8757634e487b7160e01b600052602260045260246000fd5b50919050565b634e487b7160e01b600052604160045260246000fdfea26469706673582212205748707c9f606e71f62da98132e4a0701e7e81ba9c590f784b55a1608052b63e64736f6c63430008030033";
        public WatchContractDeploymentBase() : base(BYTECODE) { }
        public WatchContractDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class GetAssembledTimeStampFunction : GetAssembledTimeStampFunctionBase { }

    [Function("GetAssembledTimeStamp", "uint256")]
    public class GetAssembledTimeStampFunctionBase : FunctionMessage
    {
        [Parameter("uint128", "GUID", 1)]
        public virtual BigInteger GUID { get; set; }
    }

    public partial class GetClientAddressFunction : GetClientAddressFunctionBase { }

    [Function("GetClientAddress", "address")]
    public class GetClientAddressFunctionBase : FunctionMessage
    {
        [Parameter("uint128", "GUID", 1)]
        public virtual BigInteger GUID { get; set; }
    }

    public partial class GetCreationAddressFunction : GetCreationAddressFunctionBase { }

    [Function("GetCreationAddress", "string")]
    public class GetCreationAddressFunctionBase : FunctionMessage
    {
        [Parameter("uint128", "GUID", 1)]
        public virtual BigInteger GUID { get; set; }
    }

    public partial class GetDeliveredTimeStampFunction : GetDeliveredTimeStampFunctionBase { }

    [Function("GetDeliveredTimeStamp", "uint256")]
    public class GetDeliveredTimeStampFunctionBase : FunctionMessage
    {
        [Parameter("uint128", "GUID", 1)]
        public virtual BigInteger GUID { get; set; }
    }

    public partial class GetDeliveryAddressFunction : GetDeliveryAddressFunctionBase { }

    [Function("GetDeliveryAddress", "string")]
    public class GetDeliveryAddressFunctionBase : FunctionMessage
    {
        [Parameter("uint128", "GUID", 1)]
        public virtual BigInteger GUID { get; set; }
    }

    public partial class GetGUIDFunction : GetGUIDFunctionBase { }

    [Function("GetGUID", "uint128")]
    public class GetGUIDFunctionBase : FunctionMessage
    {
        [Parameter("uint128", "GUID", 1)]
        public virtual BigInteger GUID { get; set; }
    }

    public partial class GetMaterialsRecievedTimeStampFunction : GetMaterialsRecievedTimeStampFunctionBase { }

    [Function("GetMaterialsRecievedTimeStamp", "uint256")]
    public class GetMaterialsRecievedTimeStampFunctionBase : FunctionMessage
    {
        [Parameter("uint128", "GUID", 1)]
        public virtual BigInteger GUID { get; set; }
    }

    public partial class GetSentTimeStampFunction : GetSentTimeStampFunctionBase { }

    [Function("GetSentTimeStamp", "uint256")]
    public class GetSentTimeStampFunctionBase : FunctionMessage
    {
        [Parameter("uint128", "GUID", 1)]
        public virtual BigInteger GUID { get; set; }
    }

    public partial class GetShippingAddressFunction : GetShippingAddressFunctionBase { }

    [Function("GetShippingAddress", "string")]
    public class GetShippingAddressFunctionBase : FunctionMessage
    {
        [Parameter("uint128", "GUID", 1)]
        public virtual BigInteger GUID { get; set; }
    }

    public partial class GetVendorAddressFunction : GetVendorAddressFunctionBase { }

    [Function("GetVendorAddress", "address")]
    public class GetVendorAddressFunctionBase : FunctionMessage
    {
        [Parameter("uint128", "GUID", 1)]
        public virtual BigInteger GUID { get; set; }
    }

    public partial class GetWatchTypeFunction : GetWatchTypeFunctionBase { }

    [Function("GetWatchType", "string")]
    public class GetWatchTypeFunctionBase : FunctionMessage
    {
        [Parameter("uint128", "GUID", 1)]
        public virtual BigInteger GUID { get; set; }
    }

    public partial class StringsAreEqualFunction : StringsAreEqualFunctionBase { }

    [Function("StringsAreEqual", "bool")]
    public class StringsAreEqualFunctionBase : FunctionMessage
    {
        [Parameter("string", "str", 1)]
        public virtual string Str { get; set; }
        [Parameter("string", "other", 2)]
        public virtual string Other { get; set; }
    }

    public partial class WatchAssembledFunction : WatchAssembledFunctionBase { }

    [Function("WatchAssembled")]
    public class WatchAssembledFunctionBase : FunctionMessage
    {
        [Parameter("address", "vendorAddress", 1)]
        public virtual string VendorAddress { get; set; }
        [Parameter("string", "physicalAddress", 2)]
        public virtual string PhysicalAddress { get; set; }
        [Parameter("uint128", "WatchGUID", 3)]
        public virtual BigInteger WatchGUID { get; set; }
    }

    public partial class WatchRecievedFunction : WatchRecievedFunctionBase { }

    [Function("WatchRecieved")]
    public class WatchRecievedFunctionBase : FunctionMessage
    {
        [Parameter("address", "vendorAddress", 1)]
        public virtual string VendorAddress { get; set; }
        [Parameter("uint128", "WatchGUID", 2)]
        public virtual BigInteger WatchGUID { get; set; }
    }

    public partial class WatchSentFunction : WatchSentFunctionBase { }

    [Function("WatchSent")]
    public class WatchSentFunctionBase : FunctionMessage
    {
        [Parameter("address", "vendorAddress", 1)]
        public virtual string VendorAddress { get; set; }
        [Parameter("uint128", "WatchGUID", 2)]
        public virtual BigInteger WatchGUID { get; set; }
        [Parameter("string", "sendingAddress", 3)]
        public virtual string SendingAddress { get; set; }
        [Parameter("string", "recievingAddress", 4)]
        public virtual string RecievingAddress { get; set; }
    }

    public partial class CreateWatchFunction : CreateWatchFunctionBase { }

    [Function("createWatch")]
    public class CreateWatchFunctionBase : FunctionMessage
    {
        [Parameter("address", "vendorAddress", 1)]
        public virtual string VendorAddress { get; set; }
        [Parameter("uint128", "GUID", 2)]
        public virtual BigInteger GUID { get; set; }
        [Parameter("string", "watchType", 3)]
        public virtual string WatchType { get; set; }
    }

    public partial class MaterialsRecievedFunction : MaterialsRecievedFunctionBase { }

    [Function("materialsRecieved")]
    public class MaterialsRecievedFunctionBase : FunctionMessage
    {
        [Parameter("address", "vendorAddress", 1)]
        public virtual string VendorAddress { get; set; }
        [Parameter("uint128", "GUID", 2)]
        public virtual BigInteger GUID { get; set; }
    }

    public partial class GetAssembledTimeStampOutputDTO : GetAssembledTimeStampOutputDTOBase { }

    [FunctionOutput]
    public class GetAssembledTimeStampOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetClientAddressOutputDTO : GetClientAddressOutputDTOBase { }

    [FunctionOutput]
    public class GetClientAddressOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetCreationAddressOutputDTO : GetCreationAddressOutputDTOBase { }

    [FunctionOutput]
    public class GetCreationAddressOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetDeliveredTimeStampOutputDTO : GetDeliveredTimeStampOutputDTOBase { }

    [FunctionOutput]
    public class GetDeliveredTimeStampOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetDeliveryAddressOutputDTO : GetDeliveryAddressOutputDTOBase { }

    [FunctionOutput]
    public class GetDeliveryAddressOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetGUIDOutputDTO : GetGUIDOutputDTOBase { }

    [FunctionOutput]
    public class GetGUIDOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint128", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetMaterialsRecievedTimeStampOutputDTO : GetMaterialsRecievedTimeStampOutputDTOBase { }

    [FunctionOutput]
    public class GetMaterialsRecievedTimeStampOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetSentTimeStampOutputDTO : GetSentTimeStampOutputDTOBase { }

    [FunctionOutput]
    public class GetSentTimeStampOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetShippingAddressOutputDTO : GetShippingAddressOutputDTOBase { }

    [FunctionOutput]
    public class GetShippingAddressOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetVendorAddressOutputDTO : GetVendorAddressOutputDTOBase { }

    [FunctionOutput]
    public class GetVendorAddressOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetWatchTypeOutputDTO : GetWatchTypeOutputDTOBase { }

    [FunctionOutput]
    public class GetWatchTypeOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class StringsAreEqualOutputDTO : StringsAreEqualOutputDTOBase { }

    [FunctionOutput]
    public class StringsAreEqualOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }










}
