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
            Money fundsToSpend = Money.Parse(_Amount.ToString());
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


            var coinslist = new List<Coin>();
            foreach (UnspentOutputReference item in MyWallet.UnspentOutputReferences
              .OrderByDescending(a => a.Confirmations > 0)
              .ThenByDescending(a => a.Transaction.Amount))
            {


                coinslist.Add(new Coin(item.Transaction.Id, (uint)item.Transaction.Index, item.Transaction.Amount, _myKey.ScriptPubKey));

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

            var signingKeys = new HashSet<ISecret>();
            var added = new HashSet<HdAddress>();


            try
            {
                ExtKey extKey = new ExtKey(_myKey, Convert.FromBase64String(MyWallet.ChainCode));

                foreach (Coin coinSpent in coinslist)
                {

                    HdAddress address = MyWallet.UnspentOutputReferences.First(output => output.ToOutPoint() == coinSpent.Outpoint).Address;

                    BitcoinExtKey addressPrivateKey = extKey.GetWif(network);
                    signingKeys.Add(addressPrivateKey);
                    added.Add(address);
                }
                //   context.TransactionBuilder.AddKeys(signingKeys.ToArray());
            }
            catch { }

            var txBuilder = new TransactionBuilder(network);
            

            var tx = txBuilder
                .AddCoins(coinslist)
                .AddKeys(signingKeys.ToArray())
                .Send(foreignAddress, fundsToSpend)
                .SendFees(feeForMiner)
                .SetChange(_myKey.ScriptPubKey)                 
                .BuildTransaction(sign: false);


            //tx.Outputs.Add(new TxOut(Money.Coins(sum), myScriptPubKey));

            //Coin[] coins = transaction.Outputs.AsCoins().ToArray();




            var resTransaction = txBuilder.Verify(tx, out errors); //check fully signed


            if (resTransaction)
            {
                var _TX = tx.GetHash().ToString();
                var transactionHex = tx.ToHex();
                return transactionHex;

            }

            return "";
        }

       


        public void SendCoins1(string _Password , HdAddress _ChangedAdress, string DistanationAddress, WalletFile MyWallet )
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




            

            var coinslist = new List<Coin>();
            foreach (UnspentOutputReference item in MyWallet.UnspentOutputReferences
              .OrderByDescending(a => a.Confirmations > 0)
              .ThenByDescending(a => a.Transaction.Amount))
            {
                

                coinslist.Add(new Coin(item.Transaction.Id, (uint)item.Transaction.Index, item.Transaction.Amount, item.Transaction.ScriptPubKey));
             
            }


            List<OutPoint> outPoints = new List<OutPoint>();
            foreach (var _coins in MyWallet.UnspentOutputReferences)
            {
                OutPoint outpoint = new OutPoint(_coins.Transaction.Id, _coins.Transaction.Index);
                outPoints.Add(outpoint);
            }


            var txBuilder = new TransactionBuilder(network);
            
            var tx = txBuilder
                .AddCoins(coinslist)
                .AddKeys(key)              
                .Send(foreignAddress.ScriptPubKey, fundsToSpend)
                .SendFees(feeForMiner)
                .SetChange(ownAddress.ScriptPubKey)
                .Shuffle()
                .BuildTransaction(sign: true);
            //tx.AddInput(new TxIn(outPoints.FirstOrDefault()));



            TransactionPolicyError[] errors = null;
            var resTransaction = txBuilder.Verify(tx, out errors); //check fully signed


            if (resTransaction)
            {
                var _TX = tx.GetHash().ToString();
                var transactionHex = tx.ToHex();
               

            }
        }



        public String TXSendCoins(string _Password, String _Amount, HdAddress _ChangedAdress, string DistanationAddress, WalletFile MyWallet)
        {
            try
            {
                Network network = new BlockCoreNetworks().GetNetwork(MyWallet.Network);


                //  Money avaiableFunds = Money.Parse("8.99990000");
                Money amountToSend = Money.Parse(_Amount.ToString());
                Money feeForMiner = Money.Parse("0.0001");

                Key _myKey;

                try
                {
                    _myKey = NBitcoin.Key.Parse(MyWallet.EncryptedSeed, _Password, network);
                }
                catch (Exception ex)
                {

                    return "";
                }

                BitcoinAddress addressToSend;
                try
                {
                    addressToSend = BitcoinAddress.Create(DistanationAddress, network);
                }
                catch (Exception ex)
                {

                    return "";
                }

                ExtKey extKey = new ExtKey(_myKey, Convert.FromBase64String(MyWallet.ChainCode));
                var signingKeys = new HashSet<ISecret>();
                var added = new HashSet<HdAddress>();

                var coinsToSpend = new HashSet<Coin>();
                bool haveEnough = SelectCoins(ref coinsToSpend, amountToSend, MyWallet.UnspentOutputReferences);



                List<HdAddress> alladress = new List<HdAddress>();
                alladress.AddRange(MyWallet.hdAccount.InternalAddresses);
                alladress.AddRange(MyWallet.hdAccount.ExternalAddresses);

                //foreach (var coin in coinsToSpend)
                //{
                //    foreach (var elem in alladress)
                //    {
                //        if (elem.ScriptPubKey == coin.ScriptPubKey)
                //        {
                //            BitcoinExtKey addressPrivateKey = extKey.GetWif(network);
                //            signingKeys.Add(addressPrivateKey);
                //        }
                //    }
                //}
                             
  
                try
                {
                    foreach (Coin coinSpent in coinsToSpend)
                    {
                        HdAddress address = MyWallet.UnspentOutputReferences.First(output => output.ToOutPoint() == coinSpent.Outpoint).Address;
                        if (added.Contains(address))
                            continue;



                        ExtKey addressExtKey = extKey.Derive(new KeyPath(address.HdPath));
                        BitcoinExtKey addressPrivateKey = addressExtKey.GetWif(network);
                        signingKeys.Add(addressPrivateKey);
                        added.Add(address);


                        //BitcoinExtKey addressPrivateKey = extKey.GetWif(network);
                        //signingKeys.Add(addressPrivateKey);
                        //added.Add(address);
                    }

                }
                catch { }



                var builder = new TransactionBuilder(network);
                var tx = builder
                    .AddCoins(coinsToSpend)
                    .AddKeys(signingKeys.ToArray())
                    .Send(addressToSend, amountToSend)
                    .SetChange(_ChangedAdress.ScriptPubKey)
                    .SendFees(feeForMiner)
                    .BuildTransaction(true);

                TransactionPolicyError[] errors;


                var resTransaction = builder.Verify(tx, out errors); //check fully signed


                if (resTransaction)
                {
                    var _TX = tx.GetHash().ToString();
                    var transactionHex = tx.ToHex();
                    return transactionHex;

                }


            



            }
            catch { }


            return "";
        }


        public static bool SelectCoins(ref HashSet<Coin> coinsToSpend, Money totalOutAmount, List<UnspentOutputReference> unspentCoins)
        {
            var haveEnough = false;
            foreach (var coin in unspentCoins.OrderByDescending(x => x.Transaction.Amount))
            {
                var _NewCoin = new Coin(coin.Transaction.Id, (uint)coin.Transaction.Index, coin.Transaction.Amount, coin.Transaction.ScriptPubKey);

                coinsToSpend.Add(_NewCoin);
                // if doesn't reach amount, continue adding next coin
                if (coinsToSpend.Sum(x => x.Amount) < totalOutAmount) continue;
                else
                {
                    haveEnough = true;
                    break;
                }
            }

            return haveEnough;
        }



    }
}
