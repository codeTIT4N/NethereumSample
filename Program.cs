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
            GetAccountBalance(infura).Wait();
            SendTransaction(infura, pvtKey).Wait();
        }

        static async Task GetAccountBalance(string? infura)
        {
            var web3 = new Web3($"https://goerli.infura.io/v3/{infura}");
            var balance = await web3.Eth.GetBalance.SendRequestAsync(
                "0xFc768Ce3d4c838eF32e7D9b2116431F7a1Be3A6D"
            );
            Console.WriteLine($"Balance in Wei: {Web3.Convert.FromWei(balance.Value)} ETH");
        }

        static async Task SendTransaction(string? infura, string? pvtKey)
        {
            var account = new Nethereum.Web3.Accounts.Account($"0x{pvtKey}");
            var web3 = new Web3(account, $"https://goerli.infura.io/v3/{infura}");
            var toAddress = "0xc9D506209f57948a0C0df6ED45621Fb47572Af99";
            var transaction = await web3.Eth
                .GetEtherTransferService()
                .TransferEtherAndWaitForReceiptAsync(toAddress, 0.1m);
            Console.WriteLine($"Transaction Hash: {transaction.TransactionHash}");
        }
    }
}
