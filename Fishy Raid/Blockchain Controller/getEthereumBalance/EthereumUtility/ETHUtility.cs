using System;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts.CQS;
using System.Numerics;
using Nethereum.StandardTokenEIP20.CQS;
using Nethereum.Util;


namespace EthereumUtility.ETHUtility {
    public class ETHUtility {
        #region Transaction Hash Event
        public delegate void TransactionHashReady(string Hash);
        public event TransactionHashReady TransactionHashReady_Event;
        #endregion

        #region Ethereum Server Enum
        public enum EthereumServerList {
            MainNet_Ethereum_Network,
            Ropsten_Test_Network,
            Kovan_Test_Network,
            Rinkeby_Test_Network,
            Monsta
        }
        #endregion

        #region 로컬 변수

        public string PrivateKey { get; set; }

    
        public string SmartContractAddress { get; set; }

        //RPC
        public EthereumServerList Server = EthereumServerList.Monsta;
        #endregion

        #region 함수
        #region Ethereum 함수
      //Private Key
        public async Task GetAccountBalanceFromPrivateKey() {
            string serverLocation = GetServerLocationFormEnum(Server);

            try {
                var web3 = new Web3(serverLocation);

                #region Ethereum의 잔액 조회 부분
             
                var balance = await web3.Eth.GetBalance.SendRequestAsync(GetWalletAddressFromPrivateKey());
              //  Console.WriteLine($"Wei 잔고 : {balance.Value}");

                var etherAmount = Web3.Convert.FromWei(balance.Value);
              //  Console.WriteLine($"Ethereum 잔고 : {etherAmount}");
                #endregion
            }
            catch (Exception ex) {
             //   Console.WriteLine($"Error Occured ! \r\n Original Message : { ex.ToString()}");
            }

        }

    
        public async Task GetAccountBalanceFromTargetWallet(string WalletAddress) {
            string serverLocation = GetServerLocationFormEnum(Server);

            try {
                var web3 = new Web3(serverLocation);

                #region Ethereum의 잔액 조회 부분
                // Wei는 BitCoin의 사토시 처럼 존재하는 Ethereum의 최소 단위입니다.
                var balance = await web3.Eth.GetBalance.SendRequestAsync(WalletAddress);
               // Console.WriteLine($"Wei 잔고 : {balance.Value}");

                var etherAmount = Web3.Convert.FromWei(balance.Value);
               // Console.WriteLine($"Ethereum 잔고 : {etherAmount}");
                #endregion
            }
            catch (Exception ex) {
               // Console.WriteLine($"Error Occured ! \r\n Original Message : { ex.ToString()}");
            }
        }

       
        public async Task TransferEthereum(string ToAddress, float Balance, int Decimal = 0) {
            string serverLocation = GetServerLocationFormEnum(Server);

            try {
                var sendVal = Web3.Convert.ToWei(Balance);
                var account = new Account(PrivateKey);
                var web3 = new Web3(account, serverLocation);

                var transAction = await web3.Eth.TransactionManager.SendTransactionAsync(GetWalletAddressFromPrivateKey(), ToAddress, new HexBigInteger(sendVal));

                TransactionHashReady_Event(transAction);
            }

            catch (Exception ex) {
               // Console.WriteLine($"Error Occured ! \r\n Original Message : { ex.ToString()}");
            }
        }
        #endregion

        #region ERC-20 Token 
    
        public async Task GetAccountTokenBalanceFromPrivateKey() {
            if (SmartContractAddress == string.Empty || SmartContractAddress == "") {
                throw (new Exception("Token을 전송하려면 SmartContract 주소부터 입력해야합니다."));
            }

            string serverLocation = GetServerLocationFormEnum(Server);

            try {
                var web3 = new Web3(serverLocation);

                var tokenService = new Nethereum.StandardTokenEIP20.StandardTokenService(web3, SmartContractAddress);
                var ownerBalanceTask = tokenService.GetBalanceOfAsync<BigInteger>(GetWalletAddressFromPrivateKey());
                var Balance = await ownerBalanceTask;

              //  Console.WriteLine(Balance.ToString());
            }
            catch (Exception ex) {
               // Console.WriteLine($"Error Occured ! \r\n Original Message : { ex.ToString()}");
            }
        }

        
        public async Task GetAccountTokenBalanceFromTargetWallet(string WalletAddress, string ContractAddress) {
            if (SmartContractAddress == string.Empty || SmartContractAddress == "") { //Ha nincs contract address
                throw (new Exception("Token을 전송하려면 SmartContract 주소부터 입력해야합니다."));
            }

            string serverLocation = GetServerLocationFormEnum(Server);

            try {
                var web3 = new Web3(serverLocation);

                var tokenService = new Nethereum.StandardTokenEIP20.StandardTokenService(web3, ContractAddress);
                var ownerBalanceTask = tokenService.GetBalanceOfAsync<BigInteger>(WalletAddress);
                var Balance = await ownerBalanceTask;

              //  Console.WriteLine(Balance.ToString());
            }
            catch (Exception ex) {
              //  Console.WriteLine($"Error Occured ! \r\n Original Message : { ex.ToString()}");
            }
        }

       
        public async Task TransferTokens(string ToAddress, UInt64 Balance) {
            string serverLocation = GetServerLocationFormEnum(Server);

          //  try {
                var account = new Account(PrivateKey);
                var web3 = new Web3(account, serverLocation);

                var transactionMessage = new TransferFunction() {
                    Value = Balance,
                    FromAddress = account.Address,
                    To = ToAddress,
                    // GasPrice = Web3.Convert.ToWei(25, UnitConversion.EthUnit.Gwei)
                    GasPrice = Web3.Convert.ToWei(0) //Ez monsta hálózatnál jó, mert ott eleve 0 a gas fee
                };

                var transferHandler = web3.Eth.GetContractTransactionHandler<TransferFunction>();

              
                var estimate = await transferHandler.EstimateGasAsync(transactionMessage, SmartContractAddress);
                 transactionMessage.Gas = estimate.Value;
            
                var transactionHash = await transferHandler.SendRequestAsync(transactionMessage, SmartContractAddress);

                TransactionHashReady_Event(transactionHash.ToString());
          /*  }
            catch (Exception ex) {
                Console.WriteLine($"Error Occured ! \r\n Original Message : { ex.ToString()}");
            }*/
        }

        
        public async Task TransferTokens(string ToAddress, UInt64 Balance, string ContractAddress) {
            string serverLocation = GetServerLocationFormEnum(Server);

            try {
                var account = new Account(PrivateKey);
                var web3 = new Web3(account, serverLocation);

                var transactionMessage = new TransferFunction() {
                    Value = (Balance * 100),
                    FromAddress = account.Address,
                    To = ToAddress,
                      GasPrice = Web3.Convert.ToWei(25, UnitConversion.EthUnit.Gwei)
                   // GasPrice = Web3.Convert.ToWei(0)
                };

                var transferHandler = web3.Eth.GetContractTransactionHandler<TransferFunction>();

           
                var estimate = await transferHandler.EstimateGasAsync(transactionMessage, ContractAddress);
                transactionMessage.Gas = estimate.Value;

                var transactionHash = await transferHandler.SendRequestAsync(transactionMessage, ContractAddress);

                TransactionHashReady_Event(transactionHash.ToString());
            }
            catch (Exception ex) {
               // Console.WriteLine($"Error Occured ! \r\n Original Message : { ex.ToString()}");
            }
        }
        #endregion

        #region Getwalletfromprivatekey
        
        private string GetWalletAddressFromPrivateKey() {
            var account = new Account(PrivateKey);

            return account.Address;
        }

        
        private string GetServerLocationFormEnum(EthereumServerList server) {
            string _server = string.Empty;

            switch (server) {
                case EthereumServerList.MainNet_Ethereum_Network:
                    _server = "https://mainnet.infura.io";
                    break;

                case EthereumServerList.Ropsten_Test_Network:
                    _server = "https://ropsten.infura.io";
                    break;

                case EthereumServerList.Kovan_Test_Network:
                    _server = "https://kovan.infura.io";
                    break;

                case EthereumServerList.Rinkeby_Test_Network:
                    _server = "https://rinkeby.infura.io";
                    break;
                case EthereumServerList.Monsta:
                    _server = "https://mainnet.monstachain.org";
                    break;
            }

            return _server;
        }
        #endregion
        #endregion
    }
}
