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

namespace Blockchain.Contracts.SimpleStorage.ContractDefinition
{


    public partial class SimpleStorageDeployment : SimpleStorageDeploymentBase
    {
        public SimpleStorageDeployment() : base(BYTECODE) { }
        public SimpleStorageDeployment(string byteCode) : base(byteCode) { }
    }

    public class SimpleStorageDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b5060d58061001f6000396000f3fe6080604052348015600f57600080fd5b5060043610603c5760003560e01c806360fe47b11460415780636d4ce63c14605d578063caa5863c146075575b600080fd5b605b60048036036020811015605557600080fd5b5035608f565b005b60636094565b60408051918252519081900360200190f35b605b60048036036020811015608957600080fd5b5035609a565b600055565b60005490565b60015556fea2646970667358221220a4759fbba711c0792c6a482b532a553203f71cd508f40ee87a1ef2cb143b747764736f6c63430007040033";
        public SimpleStorageDeploymentBase() : base(BYTECODE) { }
        public SimpleStorageDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class GetFunction : GetFunctionBase { }

    [Function("get", "uint256")]
    public class GetFunctionBase : FunctionMessage
    {

    }

    public partial class PostFunction : PostFunctionBase { }

    [Function("post")]
    public class PostFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "x", 1)]
        public virtual BigInteger X { get; set; }
    }

    public partial class SetFunction : SetFunctionBase { }

    [Function("set")]
    public class SetFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "x", 1)]
        public virtual BigInteger X { get; set; }
    }

    public partial class GetOutputDTO : GetOutputDTOBase { }

    [FunctionOutput]
    public class GetOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }




}
