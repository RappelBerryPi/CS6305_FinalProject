const HDWalletProvider = require("truffle-hdwallet-provider");
const fs = require("fs");
module.exports = {
  networks: {
    development: {
      host: "127.0.0.1",
      port: 8545,
      network_id: "*"
    },
    abs_dbrablockchainxppfll_dbrablockchainxppfll_dbrablockchainxppfll: {
      network_id: "*",
      gasPrice: 0,
      provider: new HDWalletProvider(fs.readFileSync('/home/ryan/CS6305_BlockChain_Project/myblockchainmember.env', 'utf-8'), "https://dbrablockchainxppfll.blockchain.azure.com:3200/Vhrludk2O09-0hZ7aDK1y9ki")
    }
  },
  compilers: {
    solc: {
      version: "0.7.0"
    }
  }
};
