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
        public static string BYTECODE = "608060405234801561001057600080fd5b50610fd5806100206000396000f3fe608060405234801561001057600080fd5b506004361061010b5760003560e01c80638913f84b116100a2578063bb259e4211610071578063bb259e42146102ca578063d02abe41146102f6578063dd448c6514610309578063ee981c501461031c578063f58e75241461032f57600080fd5b80638913f84b146102655780638f174f30146102915780639b7cda07146102a4578063b3b4da13146102b757600080fd5b806357c86401116100de57806357c86401146101b05780637e05646a146101d35780637ecd4fd2146101e657806383739b661461023357600080fd5b80631910668d146101105780633728eca11461014f578063519e4db81461016f578063520710e71461019b575b600080fd5b61013c61011e366004610eb9565b6001600160801b031660009081526020819052604090206009015490565b6040519081526020015b60405180910390f35b61016261015d366004610eb9565b610376565b6040516101469190610eef565b61013c61017d366004610eb9565b6001600160801b03166000908152602081905260409020600a015490565b6101ae6101a9366004610d48565b610425565b005b6101c36101be366004610e58565b610558565b6040519015158152602001610146565b6101626101e1366004610eb9565b6105b1565b61021b6101f4366004610eb9565b6001600160801b03166000908152602081905260409020600101546001600160a01b031690565b6040516001600160a01b039091168152602001610146565b61021b610241366004610eb9565b6001600160801b03166000908152602081905260409020546001600160a01b031690565b61013c610273366004610eb9565b6001600160801b031660009081526020819052604090206003015490565b61016261029f366004610eb9565b6105db565b6101ae6102b2366004610cec565b610605565b6101ae6102c5366004610d7a565b610750565b61013c6102d8366004610eb9565b6001600160801b031660009081526020819052604090206008015490565b610162610304366004610eb9565b6107ea565b6101ae610317366004610dd6565b610814565b6101ae61032a366004610d48565b610a14565b61035e61033d366004610eb9565b6001600160801b039081166000908152602081905260409020600201541690565b6040516001600160801b039091168152602001610146565b6001600160801b03811660009081526020819052604090206005018054606091906103a090610f4e565b80601f01602080910402602001604051908101604052809291908181526020018280546103cc90610f4e565b80156104195780601f106103ee57610100808354040283529160200191610419565b820191906000526020600020905b8154815290600101906020018083116103fc57829003601f168201915b50505050509050919050565b60006104318383610ab8565b42600a820155600280820180546001600160801b039081166000908152602081905260409020845481546001600160a01b03199081166001600160a01b0392831617835560018088015490840180549092169216919091179055915492820180546001600160801b031916939091169290921790915560038083015490820155600480830180549394508493918301916104ca90610f4e565b6104d5929190610b1e565b5060058201816005019080546104ea90610f4e565b6104f5929190610b1e565b50600682018160060190805461050a90610f4e565b610515929190610b1e565b50600782018160070190805461052a90610f4e565b610535929190610b1e565b506008828101549082015560098083015490820155600a91820154910155505050565b60008160405160200161056b9190610ed3565b60405160208183030381529060405280519060200120836040516020016105929190610ed3565b6040516020818303038152906040528051906020012014905092915050565b6001600160801b03811660009081526020819052604090206004018054606091906103a090610f4e565b6001600160801b03811660009081526020819052604090206007018054606091906103a090610f4e565b60006106118483610ab8565b426008820155835190915061062f9060058301906020860190610ba9565b50600280820180546001600160801b039081166000908152602081905260409020845481546001600160a01b03199081166001600160a01b0392831617835560018088015490840180549092169216919091179055915492820180546001600160801b03191693909116929092179091556003808301549082015560048083018054849392830191906106c190610f4e565b6106cc929190610b1e565b5060058201816005019080546106e190610f4e565b6106ec929190610b1e565b50600682018160060190805461070190610f4e565b61070c929190610b1e565b50600782018160070190805461072190610f4e565b61072c929190610b1e565b506008828101549082015560098083015490820155600a9182015491015550505050565b6001600160801b03808316600081815260208190526040902060020154909116141561077b57600080fd5b6001600160801b03821660008181526020818152604090912080546001600160a01b0387166001600160a01b0319918216178255600182018054909116331790556002810180546001600160801b03191690931790925582516107e49260040191840190610ba9565b50505050565b6001600160801b03811660009081526020819052604090206006018054606091906103a090610f4e565b60006108208585610ab8565b90506108b881600501805461083490610f4e565b80601f016020809104026020016040519081016040528092919081815260200182805461086090610f4e565b80156108ad5780601f10610882576101008083540402835291602001916108ad565b820191906000526020600020905b81548152906001019060200180831161089057829003601f168201915b505050505084610558565b6108c157600080fd5b42600982015582516108dc9060068301906020860190610ba9565b5081516108f29060078301906020850190610ba9565b50600280820180546001600160801b039081166000908152602081905260409020845481546001600160a01b03199081166001600160a01b0392831617835560018088015490840180549092169216919091179055915492820180546001600160801b031916939091169290921790915560038083015490820155600480830180548493928301919061098490610f4e565b61098f929190610b1e565b5060058201816005019080546109a490610f4e565b6109af929190610b1e565b5060068201816006019080546109c490610f4e565b6109cf929190610b1e565b5060078201816007019080546109e490610f4e565b6109ef929190610b1e565b506008828101549082015560098083015490820155600a918201549101555050505050565b6000610a208383610ab8565b426003808301918255600280840180546001600160801b039081166000908152602081905260409020865481546001600160a01b03199081166001600160a01b039283161783556001808a015490840180549092169216919091179055915492820180546001600160801b0319169390911692909217909155915490820155600480830180549394508493918301916104ca90610f4e565b60006001600160801b038216610acd57600080fd5b6001600160801b038216600090815260208190526040902080546001600160a01b03858116911614610afe57600080fd5b60018101546001600160a01b03163314610b1757600080fd5b9392505050565b828054610b2a90610f4e565b90600052602060002090601f016020900481019282610b4c5760008555610b99565b82601f10610b5d5780548555610b99565b82800160010185558215610b9957600052602060002091601f016020900482015b82811115610b99578254825591600101919060010190610b7e565b50610ba5929150610c1d565b5090565b828054610bb590610f4e565b90600052602060002090601f016020900481019282610bd75760008555610b99565b82601f10610bf057805160ff1916838001178555610b99565b82800160010185558215610b99579182015b82811115610b99578251825591602001919060010190610c02565b5b80821115610ba55760008155600101610c1e565b80356001600160a01b0381168114610c4957600080fd5b919050565b600082601f830112610c5e578081fd5b813567ffffffffffffffff80821115610c7957610c79610f89565b604051601f8301601f19908116603f01168101908282118183101715610ca157610ca1610f89565b81604052838152866020858801011115610cb9578485fd5b8360208701602083013792830160200193909352509392505050565b80356001600160801b0381168114610c4957600080fd5b600080600060608486031215610d00578283fd5b610d0984610c32565b9250602084013567ffffffffffffffff811115610d24578283fd5b610d3086828701610c4e565b925050610d3f60408501610cd5565b90509250925092565b60008060408385031215610d5a578182fd5b610d6383610c32565b9150610d7160208401610cd5565b90509250929050565b600080600060608486031215610d8e578283fd5b610d9784610c32565b9250610da560208501610cd5565b9150604084013567ffffffffffffffff811115610dc0578182fd5b610dcc86828701610c4e565b9150509250925092565b60008060008060808587031215610deb578081fd5b610df485610c32565b9350610e0260208601610cd5565b9250604085013567ffffffffffffffff80821115610e1e578283fd5b610e2a88838901610c4e565b93506060870135915080821115610e3f578283fd5b50610e4c87828801610c4e565b91505092959194509250565b60008060408385031215610e6a578182fd5b823567ffffffffffffffff80821115610e81578384fd5b610e8d86838701610c4e565b93506020850135915080821115610ea2578283fd5b50610eaf85828601610c4e565b9150509250929050565b600060208284031215610eca578081fd5b610b1782610cd5565b60008251610ee5818460208701610f22565b9190910192915050565b6020815260008251806020840152610f0e816040850160208701610f22565b601f01601f19169190910160400192915050565b60005b83811015610f3d578181015183820152602001610f25565b838111156107e45750506000910152565b600181811c90821680610f6257607f821691505b60208210811415610f8357634e487b7160e01b600052602260045260246000fd5b50919050565b634e487b7160e01b600052604160045260246000fdfea2646970667358221220c7beb181f4c1bb52cee00224c286b7a33ec1ae2268cd4cff53f4077842b4193464736f6c63430008040033";
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

    public partial class GetMaterialsReceivedTimeStampFunction : GetMaterialsReceivedTimeStampFunctionBase { }

    [Function("GetMaterialsReceivedTimeStamp", "uint256")]
    public class GetMaterialsReceivedTimeStampFunctionBase : FunctionMessage
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

    public partial class WatchReceivedFunction : WatchReceivedFunctionBase { }

    [Function("WatchReceived")]
    public class WatchReceivedFunctionBase : FunctionMessage
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
        [Parameter("string", "receivingAddress", 4)]
        public virtual string ReceivingAddress { get; set; }
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

    public partial class MaterialsReceivedFunction : MaterialsReceivedFunctionBase { }

    [Function("materialsReceived")]
    public class MaterialsReceivedFunctionBase : FunctionMessage
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

    public partial class GetMaterialsReceivedTimeStampOutputDTO : GetMaterialsReceivedTimeStampOutputDTOBase { }

    [FunctionOutput]
    public class GetMaterialsReceivedTimeStampOutputDTOBase : IFunctionOutputDTO 
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
