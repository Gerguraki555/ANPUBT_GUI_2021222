using System;
using EthereumUtility.ETHUtility;
using EthereumUtility.AccountUtility;

namespace EthereumBalance {
    class Program {
        public static ETHUtility eth;
        public static AccountUtility acc;

        static void Main(string[] args) {
            #region Generator
            eth = new ETHUtility();

           
            eth.PrivateKey = "190eb489859ae8b5b19a1a4993c72ec176a7bac55bca1e2b16cd4dda64daf78b";

        
            eth.SmartContractAddress = "0xae8b6389A951eF58E997bbBB886E721be5F33a0D";

        
            eth.Server = ETHUtility.EthereumServerList.Monsta;

           
            string toAddr = "0xbD438b70Da9154F6AF49a5170Ba76ba38e6a653E";

            
            eth.TransactionHashReady_Event += Eth_TransactionHashReady_Event;
            #endregion

            #region Ethereum 지갑 생성 코드

            //acc = new AccountUtility();
            //AccountInfo temp = acc.CreateEthereumAccount();

           // Console.WriteLine($"Private Key : {temp.privateKey.ToString()} \r\nWalletAddress : {temp.accountAddress.ToString()}");
            #endregion

            #region Ethereum 전송 및 조회 샘플 코드
            // 잔액 조회 
            eth.GetAccountBalanceFromPrivateKey().Wait();

            
        //    float amount = 0.5f;
         //   eth.TransferEthereum(toAddr, amount).Wait();
            #endregion

            #region ERC-20 Token 

            eth.GetAccountTokenBalanceFromPrivateKey().Wait();

            ulong amount = 100000000; //1STT

            eth.TransferTokens(toAddr, amount).Wait();
            #endregion

            Console.WriteLine("Processes all over.");
            Console.ReadLine();
        }

        #region Delegate Callback event
   
        private static void Eth_TransactionHashReady_Event(string Hash) {
            Console.WriteLine($"Transaction Hash : {Hash}");

            #region Transaction Hash 수신 후 EtherScan.io에서 각 서버별 Transaction 확인
            string server = string.Empty;

            switch (eth.Server) {
                case ETHUtility.EthereumServerList.Ropsten_Test_Network:
                    server = "ropsten.";
                    break;

                case ETHUtility.EthereumServerList.Kovan_Test_Network:
                    server = "kovan.";
                    break;

                case ETHUtility.EthereumServerList.Rinkeby_Test_Network:
                    server = "rinkeby.";
                    break;
                case ETHUtility.EthereumServerList.Monsta:
                    server = "monsta.";
                    break;
            }

            System.Diagnostics.Process.Start("explorer.exe", string.Format("https://{0}etherscan.io/tx/{1}", server, Hash));
            #endregion
        }
        #endregion
    }
}