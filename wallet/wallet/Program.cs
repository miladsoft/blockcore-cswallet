





using Blockcore.Consensus.ScriptInfo;
using Blockcore.Consensus.TransactionInfo;
using Blockcore.Networks;
using NBitcoin;
using NBitcoin.DataEncoders;
using NBitcoin.OpenAsset;
using NBitcoin.Policy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using wallet.core;

namespace wallet
{
    internal class Program
    {
        static async Task Main(string[] args)
        {



            WalletManager _WalletManager = new WalletManager();


            BlockCoreWallet _WalletObject = new BlockCoreWallet();
            //   _WalletObject.mnemonic = WalletManager.GenerateMnemonic(wordCount: 24);
            _WalletObject.mnemonic = new NBitcoin.Mnemonic("travel west flush element churn hunt certain need frame noodle aisle slender jewel museum universe amused happy ability lyrics finger behave roast surge zebra");
            _WalletObject.Passphrase = "P@@sword!!200";
            _WalletObject.Password = "P@@sword!!200";
            _WalletObject.WalletName = "WalletSBC";
            _WalletObject.NetworkName = "SeniorBlockCoinMain";
            String _MyBech32Address = "sbc1q0n7v63javqagyr3gkuz2gqeevv3ver4hrq0s9c"; // Another Wallet Adress
            String _ChangedBech32Address = "sbc1qknsjhqeyt7m9dvxxrtfq4p300tf3p32tq8m8ky"; // Another Wallet Adress
            String _TargetBech32Address = "sbc1qj04a665g70tfcmrejrvmfrugrnyc3c8rfugdlr"; // Mohsen




            //Boolean _Success = _WalletManager.CreateNewWallet(_WalletObject);
            //if (_Success == false)
            //{
            //    Console.WriteLine("Wallet Not Created");
            //    return;
            //}


            _WalletObject = _WalletManager.LoadWallet(_WalletObject);

            if (_WalletObject == null)
            {
                Console.WriteLine("Wallet Not Loaded");
                return;
            }

          SendCoins(_ChangedBech32Address, _TargetBech32Address, _WalletObject);
           
            return;

            var _balance = await new BlockCoreNewTrx().GetWalletBalance(_WalletObject);




            foreach (var _adr in _WalletObject.AddressValues)
            {
                await new BlockCoreNewTrx().SyncAdressTransection(_WalletObject, _adr.Address);

            }



            //foreach(var _adr in _LoadWallet.ReceivingAddresses)
            //{
            //    Console.WriteLine(_adr.Bech32Address);
            //}

            //Console.WriteLine("");

            //foreach (var _adr in _LoadWallet.ChangeAddresses)
            //{
            //    Console.WriteLine(_adr.Bech32Address);
            //}



            var SourceAddress = _WalletObject.ReceivingAddresses.FirstOrDefault(x => x.Bech32Address == _MyBech32Address);
            var changeAddress = _WalletObject.ChangeAddresses.FirstOrDefault(x => x.Bech32Address == _ChangedBech32Address);


            // Class 1 to CreateNewTransection
            //var _Data = _WalletManager.CreateNewTransection(_LoadWallet, _TargetBech32Address, changeAddress, Money.Parse("9.9999"));


            // Class 2 to CreateNewTransection
            // new BlockCoreTransection().CreateNewTransection(_WalletObject, _TargetBech32Address, SourceAddress, changeAddress );
            //new BlockCoreTransection().CreateNewTransection(_WalletObject, _TargetBech32Address, SourceAddress, changeAddress);

            //new BlockCoreTransection().RecoverTransection("", _WalletObject);
            //  new BlockCoreTransection().SendBTC(_WalletObject, _TargetBech32Address, SourceAddress, decimal.Parse("9.999"), changeAddress);


            var transactionResult = new BlockCoreNewTrx().CreateNewTransection(_WalletObject, _TargetBech32Address, _MyBech32Address, changeAddress);

            if (transactionResult != null)
            {
                var Hex = transactionResult.ToHex();

                var TransactionId = transactionResult.GetHash();

                //    new BlockCoreTransection().RecoverTransection("", _WalletObject);
                new BlockCoreTransection().RecoverTransection(Hex, _WalletObject);

            }
            else
            {
                Console.WriteLine("Error");
            }
            Console.ReadLine();
        }

  

        public static void SendCoins(string OwnAddress, string ForeignAddress, BlockCoreWallet _WalletObject)
        {
            Network net = GetNetwork();


            Money avaiableFunds = Money.Parse("8.99990000");
            Money fundsToSpend = Money.Parse("5");
            Money feeForMiner = Money.Parse("0.0001");

     
            var ownAddress = BitcoinAddress.Create(OwnAddress, net);
            var foreignAddress = BitcoinAddress.Create(ForeignAddress, net);


            // utxo
            //https://sbc.indexer.blockcore.net/api/query/address/sbc1q0n7v63javqagyr3gkuz2gqeevv3ver4hrq0s9c/transactions?offset=0&limit=10

            var coin = new Coin(fromTxHash: new uint256("54d138222810c3672d93cadc7aaace73111017ff978202ab1e4beb00fe29e46b"),
                fromOutputIndex: 127518,                
                amount: Money.Parse("8.99990000") ,
                scriptPubKey: _WalletObject.PrivateKey.ScriptPubKey
                );


           
            var txBuilder = new TransactionBuilder(GetNetwork());
            var tx = txBuilder
                .AddCoins(coin)
                .AddKeys(_WalletObject.PrivateKey)
                .Send(foreignAddress, fundsToSpend)
                .SendFees(feeForMiner)
                .SetChange(ownAddress)              
                .BuildTransaction(sign: true);

            TransactionPolicyError[] errors = null;
            var resTransaction = txBuilder.Verify(tx, out errors); //check fully signed


            if (resTransaction)
            {
                var _TX = tx.GetHash().ToString();
                var transactionHex = tx.ToHex();
                new BlockCoreTransection().RecoverTransection(transactionHex, _WalletObject);

            }
        }



     
        public static Blockcore.Networks.Network GetNetwork(String _NetworkName = "SeniorBlockCoinMain")
        {
            //Network networkSBC = Blockcore.Networks.SeniorBlockCoin.Networks.Networks.SeniorBlockCoin.Mainnet();

            if (_NetworkName == "SeniorBlockCoinMain")
            {
                return Blockcore.Networks.SeniorBlockCoin.Networks.Networks.SeniorBlockCoin.Mainnet();
            }

            return Blockcore.Networks.SeniorBlockCoin.Networks.Networks.SeniorBlockCoin.Mainnet();
        }
    }
}


/*
 

*/