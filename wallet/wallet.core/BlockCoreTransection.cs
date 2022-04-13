using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blockcore.Connection.Broadcasting;
using Blockcore.Consensus;
using Blockcore.Consensus.ScriptInfo;
using Blockcore.Consensus.TransactionInfo;
using Blockcore.Features.Wallet;
using Blockcore.Features.Wallet.Database;
using Blockcore.Features.Wallet.Types;
using Blockcore.Networks;
using NBitcoin;
using NBitcoin.DataEncoders;
using NBitcoin.Policy;
using Newtonsoft.Json;

namespace wallet.core
{
    public class BlockCoreTransection
    {

        public void RecoverTransection(String transactionHex, BlockCoreWallet _BlockCoreWallet)
        {
            try
            {


                if (transactionHex == "")
                {//sample

                  //  transactionHex = "010000000888526200010138ecd19af24cc2564653d6dd664a374ac0b199d8560dbb0fac385b38478b84e70100000000ffffffff036043993b0000000016001458422b8a2a7756bd2a94e81e2fb7d9ac838fe8930000000000000000026a00905f010000000000160014d264dc1fd05ac6dcccb7b29f56b24a8a145fa64f0247304402204ab63b6ca99e156983d214c18a88ad1a00e832941b96f1798a2bb9a60993cf300220670146dc660fe507425a6d1eef6a175f9d95ea95c8eebdf8b80e3c3b7557e74f012103faf77c95d4d4d3bf4887cb640717418fac051c324a36665b693d91cc931a825900000000";
                }
                 //  transactionHex = "010000000888526200010138ecd19af24cc2564653d6dd664a374ac0b199d8560dbb0fac385b38478b84e70100000000ffffffff036043993b0000000016001458422b8a2a7756bd2a94e81e2fb7d9ac838fe8930000000000000000026a00905f010000000000160014d264dc1fd05ac6dcccb7b29f56b24a8a145fa64f0247304402204ab63b6ca99e156983d214c18a88ad1a00e832941b96f1798a2bb9a60993cf300220670146dc660fe507425a6d1eef6a175f9d95ea95c8eebdf8b80e3c3b7557e74f012103faf77c95d4d4d3bf4887cb640717418fac051c324a36665b693d91cc931a825900000000";

                var _Network = GetNetwork(_BlockCoreWallet.NetworkName);

                Transaction transaction = _Network.CreateTransaction(transactionHex);
                TxOut output1 = transaction.Outputs[0];
                TxIn input1 = transaction.Inputs[0];
                

                var TransactionId = transaction.GetHash();
                var Outputs = transaction.Outputs;

                
            }
            catch { }
        }
        public void CreateNewTransectionTRXA(BlockCoreWallet _WalletObject, String _secretAddress, HdAddress _SourceAddress, HdAddress changeAddress)
        {
            try
            {
                var _Network = GetNetwork(_WalletObject.NetworkName);

                var _secret = BitcoinAddress.Create(_secretAddress, _Network);
                var txGettingScriptCoinForBobAlice = new Transaction();
                txGettingScriptCoinForBobAlice.Outputs.Add(new TxOut(Money.Coins(10m), _SourceAddress.ScriptPubKey));

               var coins = txGettingScriptCoinForBobAlice.Outputs.AsCoins().ToArray();
                ScriptCoin bobAliceScriptCoin = coins[0].ToScriptCoin(_SourceAddress.ScriptPubKey);

                //Then the signature:
                var builderForSendingScriptCoinToSatoshi = new TransactionBuilder(_Network);
                var txForSendingScriptCoinToSatoshi = builderForSendingScriptCoinToSatoshi
                        .AddCoins(bobAliceScriptCoin)
                        .AddKeys(_WalletObject.PrivateKey)
                        .Send(_secret.ScriptPubKey, Money.Coins(9.999m))
                        .SetChange(_SourceAddress.ScriptPubKey)
                        .SendFees(Money.Coins(0.0001m))
                        .BuildTransaction(true);

                Console.WriteLine(builderForSendingScriptCoinToSatoshi.Verify(txForSendingScriptCoinToSatoshi));

                TransactionPolicyError[] errors = null;
                if (!builderForSendingScriptCoinToSatoshi.Verify(txForSendingScriptCoinToSatoshi, out errors))
                {
                    Console.WriteLine("Couldn't build the transaction.");
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error.ToString());
                    }
                }

                var TransactionHash = txForSendingScriptCoinToSatoshi.GetHash();
                var TransactionHex = txForSendingScriptCoinToSatoshi.ToHex();

            }
            catch (Exception _ex)
            {

            }

        }

        public void CreateNewTransectionTRX(BlockCoreWallet _BlockCoreWallet, String _TargetAddress, HdAddress _SourceAddress, HdAddress changeAddress)
        {
            try
            {
                var _Network = GetNetwork(_BlockCoreWallet.NetworkName);

                var _TaretBitcoinAddress = BitcoinAddress.Create(_TargetAddress, _Network);
                //TxIn _TxIn = new TxIn(_TaretBitcoinAddress.ScriptPubKey);
                //TxOut _TxOut = new TxOut("9.999", _SourceAddress.ScriptPubKey);



                //Transaction transaction = _Network.Consensus.ConsensusFactory.CreateTransaction();
                //transaction.AddInput(_TxIn);
                //transaction.AddOutput(_TxOut);


                Transaction aliceFunding = new Transaction()
                {
                    Inputs =
                {
                    new TxIn ( _SourceAddress.ScriptPubKey)
                }
                    ,

                    Outputs =
            {
                new TxOut(Money.Coins(0.0009m),_TaretBitcoinAddress.ScriptPubKey)
            }

                };




                Coin[] MainAddressCoins = aliceFunding
                                    .Outputs
                                    .Select((o, i) => new Coin(new OutPoint(aliceFunding.GetHash(), i), o))
                                    .ToArray();



                //   var fee = aliceFunding.GetFee(MainAddressCoins);
                ExtKey extendedKey = _BlockCoreWallet.mnemonic.DeriveExtKey(_BlockCoreWallet.Passphrase);
                var fee = aliceFunding.GetFee(MainAddressCoins);
                var txBuilder = new TransactionBuilder(_Network);
                var tx = txBuilder
                    .AddCoins(MainAddressCoins)
                    .AddKeys(extendedKey.PrivateKey)
                    .Send(_TaretBitcoinAddress.ScriptPubKey.GetScriptAddress(_Network), Money.Coins(9m))
                    .SendFees(Money.Coins(0.0001m))
                    .SetChange(changeAddress.ScriptPubKey)
                    .BuildTransaction(true);



                Transaction txs = txBuilder.BuildTransaction(false);
                TransactionPolicyError[] errors = null;
                if (!txBuilder.Verify(txs, out errors))
                {
                    Console.WriteLine("Couldn't build the transaction.");
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error.ToString());
                    }
                }



                var _TX = tx.GetHash().ToString();
                var transactionHex = tx.ToHex();

            }
            catch (Exception _ex)
            {

            }

        }

        public void SendBTC(BlockCoreWallet _WalletObject, string secret, HdAddress _SourceAddress, decimal amount, HdAddress changeAddress)
        {
            var _Network = GetNetwork(_WalletObject.NetworkName);
            //var bitcoinsecret = new BitcoinSecret(secret, _Network);


            //var _Taret = BitcoinAddress.Create(secret, _Network);

            //var transactionId = uint256.Parse(fundingTransactionHash);

            var _secret = BitcoinAddress.Create(secret, _Network);
            var _Source = BitcoinAddress.Create(_SourceAddress.Bech32Address, _Network);



            Transaction aliceFunding = new Transaction()
            {
                Outputs =
            {
                new TxOut(Money.Coins(9.999m), _Source.ScriptPubKey)
            }

            };
            Coin[] MainAddressCoins = aliceFunding
                                    .Outputs
                                    .Select((o, i) => new Coin(new OutPoint(aliceFunding.GetHash(), i), o))
                                    .ToArray();
            //   var fee = aliceFunding.GetFee(MainAddressCoins);
            ExtKey extendedKey = _WalletObject.mnemonic.DeriveExtKey(_WalletObject.Passphrase);
            var fee = aliceFunding.GetFee(MainAddressCoins);
            var txBuilder = new TransactionBuilder(_Network);
            var tx = txBuilder
                .AddCoins(MainAddressCoins)
                .AddKeys(extendedKey.PrivateKey)
                .Send(_secret.ScriptPubKey.GetScriptAddress(_Network), Money.Coins(9.999m))
                .SendFees(Money.Coins(0.0009m))
                .SetChange(changeAddress.ScriptPubKey)
                .BuildTransaction(true);


            Transaction txs = txBuilder.BuildTransaction(true);
            TransactionPolicyError[] errors = null;
            if (!txBuilder.Verify(txs, out errors))
            {
                Console.WriteLine("Couldn't build the transaction.");
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ToString());
                }
            }



            var _TX = tx.GetHash().ToString();
            var transactionHex = tx.ToHex();



        }

        public void CreateNewTransection(BlockCoreWallet _BlockCoreWallet, String _TargetAddress, HdAddress _SourceAddress, HdAddress changeAddress)
        {
            try
            {
                var _Network = GetNetwork(_BlockCoreWallet.NetworkName);




                ExtKey extendedKey = _BlockCoreWallet.mnemonic.DeriveExtKey(_BlockCoreWallet.Passphrase);



                Recipient TargetRecipient = new Recipient
                {
                    ScriptPubKey = BitcoinAddress.Create(_TargetAddress, _Network).ScriptPubKey,
                    Amount = Money.Coins(9.999m)
                };

                var recipients = new List<Recipient>();
                recipients.Add(TargetRecipient);

                //Money fee = Money.Parse("0.00010000");         // Fee

                //if (TransactionFee == null)
                //{
                //    //  Money minTrxFee = new Money(_Network.MinTxFee, MoneyUnit.Satoshi);
                //}


                TransactionBuilder txBuilder = new TransactionBuilder(_Network);

                var context = new TransactionBuildContext(_Network)
                {
                    AccountReference = new WalletAccountReference(_BlockCoreWallet.WalletName, "account 0"),
                    Shuffle = true, // We shuffle transaction outputs by default as it's better for anonymity.
                    WalletPassword = _BlockCoreWallet.Password,
                    Recipients = recipients,
                    UseSegwitChangeAddress = recipients[0].ScriptPubKey.IsScriptType(ScriptType.Witness),
                    ChangeAddress = _SourceAddress,
                    OpReturnData = "",
                    OpReturnRawData = new byte[] { },
                    MinConfirmations = 0,
                    FeeType = FeeType.Low

                };




                var _Source = BitcoinAddress.Create(_SourceAddress.Bech32Address, _Network);
                var _Taret = BitcoinAddress.Create(_TargetAddress, _Network);
                TxIn _TxIn = new TxIn(_SourceAddress.ScriptPubKey);
                TxOut _TxOut = new TxOut(Money.Coins(9.999m), _Taret.ScriptPubKey);

                Transaction transaction1 = new Transaction();
                transaction1.AddInput(_TxIn);
                transaction1.AddOutput(_TxOut);

                OutPoint outPoint = new OutPoint(transaction1, 0);






                UnspentOutputReference _out = new UnspentOutputReference();
                _out.Transaction = new TransactionOutputData();
                _out.Transaction.Address = _SourceAddress.Address;
                _out.Transaction.Amount = Money.Coins(9.999m);
                _out.Transaction.ScriptPubKey = _Taret.ScriptPubKey;


                _out.Account = _BlockCoreWallet.HdAccount;
                _out.Address = _SourceAddress;



                // context.UnspentOutputs = new List<UnspentOutputReference>();
                // context.UnspentOutputs.Add(_out);
               context.TransactionBuilder.AddCoins(AddCoin(_Network, _Source));
                //  context.TransactionBuilder.AddCoins(transaction1);

                // context.TransactionBuilder.AddCoins(AddCoin(_Network, _Source));


                //FeeRate feeRate = new FeeRate(fee);
                ////context.TransactionBuilder.EstimateFees(feeRate);
                //context.TransactionBuilder.SendFees(fee);




                context.TransactionBuilder.SendFees(Money.Coins(0.0009m));
                context.TransactionFee = Money.Coins(0.0009m);

                //  context.SelectedInputs.Add(outPoint);
                context.TransactionBuilder.Send(TargetRecipient.ScriptPubKey, Money.Coins(8m));
                  Transaction transaction = context.TransactionBuilder.BuildTransaction(true);

                TransactionPolicyError[] errors = null;
                if (!context.TransactionBuilder.Verify(transaction, out errors))
                {
                    Console.WriteLine("Couldn't build the transaction.");
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error.ToString());
                    }
                }



                var TransactionHash = transaction.GetHash();
                var TransactionHex = transaction.ToHex();


                var _TR = _Network.Consensus.ConsensusFactory.CreateTransaction(transaction.ToHex());
                var _TRTRX = new Transaction();
                transaction.FromBytes(Encoders.Hex.DecodeData(transaction.ToHex()));


                int _A = 100;






            }
            catch (Exception e)
            {

            }




            return;


        }












        private static ICoin[] AddCoin(Blockcore.Networks.Network _network, BitcoinAddress _TaretAddress)
        {
            TransactionBuilder builder = new TransactionBuilder(_network);
            var transaction = _network.CreateTransaction();
            transaction.Outputs.Add(new TxOut(Money.Coins(10), _TaretAddress.ScriptPubKey));
            Coin[] coins = transaction.Outputs.AsCoins().ToArray();
            return coins;
        }
















        private async Task SendTransactionToIndexer(string transactionHex)
        {
            try
            {
                var client = new HttpClient();

                var content = new System.Net.Http.StringContent(transactionHex, Encoding.UTF8, "application/json-patch+json");

                var request = new HttpRequestMessage
                {
                    Content = content,
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://sbc.indexer.seniorblockchain.io/api/command/send/"),
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    indexerAdress myDeserializedClass = JsonConvert.DeserializeObject<indexerAdress>(body.ToString());

                }
            }
            catch { }

        }



        private static HttpClient _httpClient = new HttpClient();

        public bool POSTData(object json, string url)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(json), System.Text.Encoding.UTF8, "application/json"))
            {
                HttpResponseMessage result = _httpClient.PostAsync(url, content).Result;
                if (result.StatusCode == System.Net.HttpStatusCode.Created)
                    return true;
                string returnValue = result.Content.ReadAsStringAsync().Result;
                throw new Exception($"Failed to POST data: ({result.StatusCode}): {returnValue}");
            }
        }


        public Network GetNetwork(String _NetworkName = "SeniorBlockCoinMain")
        {
            //Network networkSBC = Blockcore.Networks.SeniorBlockCoin.Networks.Networks.SeniorBlockCoin.Mainnet();

            if (_NetworkName == "SeniorBlockCoinMain")
            {
                return Blockcore.Networks.SeniorBlockCoin.Networks.Networks.SeniorBlockCoin.Mainnet();
            }

            return Blockcore.Networks.SeniorBlockCoin.Networks.Networks.SeniorBlockCoin.Mainnet();
        }
        public int CoinType(String _NetworkName = "SeniorBlockCoinMain")
        {
            if (_NetworkName == "SeniorBlockCoinMain")
            { return 5006; }



            return 5006;
        }


    }
}
