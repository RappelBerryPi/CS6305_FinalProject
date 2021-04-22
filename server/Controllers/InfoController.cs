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
using server.Services;

namespace server.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : Controller {

        private readonly ILogger<InfoController> logger;
        private ContractService ContractService { get; set; }

        public InfoController(ILogger<InfoController> logger, ContractService contractService) {
            this.logger = logger;
            this.ContractService = contractService;
        }

        [HttpGet("Get/{guid}")]
        [AllowAnonymous]
        public Watch Get(string guid) {
            return this.ContractService.GetWatch(guid);
        }

        [HttpPost("NewWatch")]
        [AllowAnonymous]
        public ShopItem NewWatch(NewWatchForm watchForm) {
            if (ModelState.IsValid) {
                return this.ContractService.NewWatch(watchForm);
            } else {
                throw new ArgumentException("invalid submission");
            }
        }

        [HttpPatch("{guid}/MaterialsReceived")]
        [AllowAnonymous]
        public Watch MaterialsReceived(string guid, BasicApiValidationForm form) {
            if (ModelState.IsValid) {
                return this.ContractService.MaterialsReceived(guid, form);
            }
            throw new ArgumentException("invalid submission");
        }

        [HttpPatch("{guid}/WatchAssembled")]
        [AllowAnonymous]
        public Watch WatchAssembled(string guid, WatchAssembledForm form) {
            if (ModelState.IsValid) {
                return this.ContractService.WatchAssembled(guid, form);
            }
            throw new ArgumentException("invalid submission");
        }

        [HttpPatch("{guid}/WatchSent")]
        [AllowAnonymous]
        public Watch WatchSent(string guid, WatchSentForm form) {
            if (ModelState.IsValid) {
                return this.ContractService.WatchSent(guid, form);
            }
            throw new ArgumentException("invalid submission");
        }


        [HttpPatch("{guid}/WatchReceived")]
        [AllowAnonymous]
        public Watch WatchReceived(string guid, BasicApiValidationForm form) {
            if (ModelState.IsValid) {
                return this.ContractService.WatchReceived(guid, form);
            }
            throw new ArgumentException("invalid submission");
        }

    }
}