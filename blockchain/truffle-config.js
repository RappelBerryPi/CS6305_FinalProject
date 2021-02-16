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
<<<<<<< HEAD
      provider: new HDWalletProvider(fs.readFileSync('C:/Users/dbrey/drewbreyer/CS6305_FinalProject/blockchain/myblockchainmember.env', 'utf-8'), "https://dbrablockchainxppfll.blockchain.azure.com:3200/Vhrludk2O09-0hZ7aDK1y9ki")
=======
      provider: new HDWalletProvider(fs.readFileSync('C:/Users/leppa/workspace/Projects/CS6305_FinalProject/myblockchainmember.env', 'utf-8'), "https://dbrablockchainxppfll.blockchain.azure.com:3200/Vhrludk2O09-0hZ7aDK1y9ki")
>>>>>>> 03660b15cc5b223618e512ed94b3f44d32504810
    }
  },
  compilers: {
    solc: {
      version: "0.7.0"
    }
  }
};
