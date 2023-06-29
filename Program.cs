using Nethereum.Web3;

namespace NethereumSample
{
    class Program
    {
        static void Main(string[] args)
        {
            DotNetEnv.Env.Load();
            var infura = Environment.GetEnvironmentVariable("INFURA");
            var pvtKey = Environment.GetEnvironmentVariable("PVT_KEY");
            var toAddress = Environment.GetEnvironmentVariable("TO_ADDRESS");
            GetAccountBalance(infura).Wait();
            SendTransaction(infura, pvtKey, toAddress).Wait();
        }

        static async Task GetAccountBalance(string? infura)
        {
            var web3 = new Web3(infura);
            var balance = await web3.Eth.GetBalance.SendRequestAsync(
                "0xFc768Ce3d4c838eF32e7D9b2116431F7a1Be3A6D"
            );
            Console.WriteLine($"Balance: {Web3.Convert.FromWei(balance.Value)} ETH");
        }

        static async Task SendTransaction(string? infura, string? pvtKey, string? toAddress)
        {
            Console.WriteLine($"Sending ether to {toAddress}...");
            var account = new Nethereum.Web3.Accounts.Account($"{pvtKey}");
            var web3 = new Web3(account, infura);
            var transaction = await web3.Eth
                .GetEtherTransferService()
                .TransferEtherAndWaitForReceiptAsync(toAddress, 0.1m);
            Console.WriteLine($"Transaction Hash: {transaction.TransactionHash}");
        }
    }
}
