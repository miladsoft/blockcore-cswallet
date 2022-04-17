using Blockcore.Consensus.TransactionInfo;
using Blockcore.Features.Wallet.Types;
using Blockcore.Networks;
using NBitcoin;
using NBitcoin.Policy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallet.core
{
    public class SendCoin
    {
        public String SendCoins(string _Password, String _Amount, HdAddress _ChangedAdress, string DistanationAddress, WalletFile MyWallet, out TransactionPolicyError[] errors)
        {
            Network network = new BlockCoreNetworks().GetNetwork(MyWallet.Network);


            //  Money avaiableFunds = Money.Parse("8.99990000");
            Money fundsToSpend = Money.Parse("5");
            Money feeForMiner = Money.Parse("0.0001");

            Key _myKey;

            try
            {
                _myKey = NBitcoin.Key.Parse(MyWallet.EncryptedSeed, _Password, network);
            }
            catch (Exception ex)
            {
                errors = new TransactionPolicyError[1];
                errors[0]= new TransactionPolicyError("Password Error");
                return "";
            }


            var ownAddress = BitcoinAddress.Create(_ChangedAdress.Bech32Address, network);
            var foreignAddress = BitcoinAddress.Create(DistanationAddress, network);

           
            List<Coin> coinList = new List<Coin>();
            foreach (var _coins in MyWallet.UnspentOutputReferences)
            {
                var newcoin = new Coin(fromTxHash: new uint256(_coins.Transaction.Id),
                  fromOutputIndex: (uint)_coins.Transaction.BlockHeight,
                  amount: Money.Parse(_coins.Transaction.Amount.ToString()),
                  scriptPubKey: _coins.Transaction.ScriptPubKey
                  );
                coinList.Add(newcoin);
            }

            // utxo
            //https://sbc.indexer.blockcore.net/api/query/address/sbc1q0n7v63javqagyr3gkuz2gqeevv3ver4hrq0s9c/transactions?offset=0&limit=10

            //var coin = new Coin(fromTxHash: new uint256("54d138222810c3672d93cadc7aaace73111017ff978202ab1e4beb00fe29e46b"),
            //    fromOutputIndex: 127518,
            //    amount: Money.Parse("8.99990000"),
            //    scriptPubKey: _WalletObject.PrivateKey.ScriptPubKey
            //    );



            //OutPoint outPoint = new OutPoint(uint256.Parse("54d138222810c3672d93cadc7aaace73111017ff978202ab1e4beb00fe29e46b"), 0);
            //TxIn _TxIn = new TxIn(outPoint);
            //TxOut _TxOut = new TxOut(Money.Parse("8.99990000"), ownAddress);



            //Transaction transaction1 = new Transaction();
            //transaction1.AddInput(_TxIn);
            //transaction1.AddOutput(_TxOut);

            //Transaction aliceFunding = new Transaction()
            //{
            //    Inputs =
            //    {
            //        new TxIn (outPoint)
            //    }
            //    ,

            //    Outputs =
            //{
            //    new TxOut(Money.Parse("8.99990000"), ownAddress)
            //}

            //};
            //Coin[] MainAddressCoins = aliceFunding
            //                        .Outputs
            //                        .Select((o, i) => new Coin(new OutPoint(aliceFunding.GetHash(), i), o))
            //                        .ToArray();



            var txBuilder = new TransactionBuilder(network);
            

            var tx = txBuilder
                .AddCoins(coinList)
                .AddKeys(_myKey)
                .Send(foreignAddress, fundsToSpend)
                .SendFees(feeForMiner)
                .SetChange(ownAddress)                 
                .BuildTransaction(sign: true);


            
            var resTransaction = txBuilder.Verify(tx, out errors); //check fully signed


            if (resTransaction)
            {
                var _TX = tx.GetHash().ToString();
                var transactionHex = tx.ToHex();
                return transactionHex;

            }

            return "";
        }


        public   void SendCoins1(string _Password , HdAddress _ChangedAdress, string DistanationAddress, WalletFile MyWallet )
        {
            Network network = new BlockCoreNetworks().GetNetwork(MyWallet.Network);
            Key key;
            try
            {
                key = NBitcoin.Key.Parse(MyWallet.EncryptedSeed, _Password, network);
            }
            catch (Exception ex)
            {
              

                return;
            }


         
            Money fundsToSpend = Money.Parse("5");
            Money feeForMiner = Money.Parse("0.0001");


            var ownAddress = BitcoinAddress.Create(_ChangedAdress.Address, network);
            var foreignAddress = BitcoinAddress.Create(DistanationAddress, network);


            // utxo
            //https://sbc.indexer.blockcore.net/api/query/address/sbc1q0n7v63javqagyr3gkuz2gqeevv3ver4hrq0s9c/transactions?offset=0&limit=10

            //var coin = new Coin(fromTxHash: new uint256("54d138222810c3672d93cadc7aaace73111017ff978202ab1e4beb00fe29e46b"),
            //    fromOutputIndex: 127518,
            //    amount: Money.Parse("8.99990000"),
            //    scriptPubKey: _WalletObject.PrivateKey.ScriptPubKey
            //    );



            List<Coin> coinList = new List<Coin>();
            foreach (var _coins in MyWallet.UnspentOutputReferences)
            {
                var newcoin = new Coin(fromTxHash: new uint256(_coins.Transaction.Id),
                  fromOutputIndex: (uint)_coins.Transaction.BlockHeight,
                  amount: Money.Parse(_coins.Transaction.Amount.ToString()),
                  scriptPubKey: _coins.Transaction.ScriptPubKey
                  );
              

                coinList.Add(newcoin);
            }








            var txBuilder = new TransactionBuilder(network);

            var tx = txBuilder
                .AddCoins(coinList)
                .AddKeys(key)
                .Send(foreignAddress, fundsToSpend)
                .SendFees(feeForMiner)
                .SetChange(ownAddress.ScriptPubKey)
                .BuildTransaction(sign: true);

            


            TransactionPolicyError[] errors = null;
            var resTransaction = txBuilder.Verify(tx, out errors); //check fully signed


            if (resTransaction)
            {
                var _TX = tx.GetHash().ToString();
                var transactionHex = tx.ToHex();
               

            }
        }

    }
}
