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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallet.core
{
    public class BlockTransection
    {
        public int currentChainHeight = 132622; // sample for sbc
        private int? confirmations;
        public async Task<List<UnspentOutputReference>> GetSpendableTransactions(WalletFile MyWallet, List<AddressBalance> addressBalances)
        {
            List<UnspentOutputReference> UnspentOutputReferences = new List<UnspentOutputReference>();
            try
            {
                Network network = new BlockCoreNetworks().GetNetwork(MyWallet.Network);
                int coinbaseMaturity = (int)network.Consensus.CoinbaseMaturity;

                foreach (var _Adr in addressBalances.Where(li => li.balance > 0))
                {
                    try
                    {

                        HdAddress hdAddress;
                        try
                        {
                            hdAddress = MyWallet.hdAccount.InternalAddresses.First(li => li.Bech32Address == _Adr.address);
                        }
                        catch { hdAddress = MyWallet.hdAccount.ExternalAddresses.First(li => li.Bech32Address == _Adr.address); }


                        var _Trxs = await GetAddressTransectionT(_Adr.address, new BlockCoreNetworks().GetNetwork(MyWallet.Network));
                        if (_Trxs != null)
                        {
                            int countFrom = currentChainHeight + 1;

                            foreach (TransactionOutputData transactionData in await UnspentTransactions(hdAddress, network, _Trxs))
                            {
                                int? confirmationCount = 0;

                                if (transactionData.BlockHeight != null)
                                {
                                    confirmationCount = countFrom >= transactionData.BlockHeight ? countFrom - transactionData.BlockHeight : 0;
                                }

                                if (confirmationCount < confirmations)
                                {
                                    continue;
                                }

                                bool isCoinBase = transactionData.IsCoinBase ?? false;
                                bool isCoinStake = transactionData.IsCoinStake ?? false;

                                // Check if this wallet is a normal purpose wallet (not cold staking, etc).
                                //if (this.IsNormalAccount())
                                //{
                                //    bool isColdCoinStake = transactionData.IsColdCoinStake ?? false;

                                //    // Skip listing the UTXO if this is a normal wallet, and the UTXO is marked as an cold coin stake.
                                //    if (isColdCoinStake)
                                //    {
                                //        continue;
                                //    }
                                //}


                                // This output can unconditionally be included in the results.
                                // Or this output is a ColdStake, CoinBase or CoinStake and has reached maturity.
                                if ((!isCoinBase && !isCoinStake) || (confirmationCount > coinbaseMaturity))
                                {
                                    UnspentOutputReferences.Add(new UnspentOutputReference
                                    {
                                        Account = MyWallet.hdAccount,
                                        Address = hdAddress,
                                        Transaction = transactionData,
                                        Confirmations = confirmationCount.Value,

                                    });
                                }
                            }


                        }

                    }
                    catch { }
                }


            }
            catch { }

            return UnspentOutputReferences;
        }

        public async Task<List<UnspentOutputReference>> GetSpendableTransactions(WalletFile MyWallet)
        {
            List<UnspentOutputReference> UnspentOutputReferences = new List<UnspentOutputReference>();
            try
            {
                Network network = new BlockCoreNetworks().GetNetwork(MyWallet.Network);
                int coinbaseMaturity = (int)network.Consensus.CoinbaseMaturity;

                foreach (var _Adr in MyWallet.hdAccount.InternalAddresses)
                {
                    try
                    {
                        AddressBalance addressBalance = await new AddressManager().GetAddressBalance(_Adr.Bech32Address);
                        if (addressBalance != null)
                        {
                            if (addressBalance.balance > 0)
                            {
                                var _Trxs = await GetAddressTransectionT(_Adr.Bech32Address, new BlockCoreNetworks().GetNetwork(MyWallet.Network));
                                if (_Trxs != null)
                                {
                                    int countFrom = currentChainHeight + 1;

                                    foreach (TransactionOutputData transactionData in await UnspentTransactions(_Adr, network, _Trxs))
                                    {
                                        int? confirmationCount = 0;

                                        if (transactionData.BlockHeight != null)
                                        {
                                            confirmationCount = countFrom >= transactionData.BlockHeight ? countFrom - transactionData.BlockHeight : 0;
                                        }

                                        if (confirmationCount < confirmations)
                                        {
                                            continue;
                                        }

                                        bool isCoinBase = transactionData.IsCoinBase ?? false;
                                        bool isCoinStake = transactionData.IsCoinStake ?? false;

                                        // Check if this wallet is a normal purpose wallet (not cold staking, etc).
                                        //if (this.IsNormalAccount())
                                        //{
                                        //    bool isColdCoinStake = transactionData.IsColdCoinStake ?? false;

                                        //    // Skip listing the UTXO if this is a normal wallet, and the UTXO is marked as an cold coin stake.
                                        //    if (isColdCoinStake)
                                        //    {
                                        //        continue;
                                        //    }
                                        //}

                                        // This output can unconditionally be included in the results.
                                        // Or this output is a ColdStake, CoinBase or CoinStake and has reached maturity.
                                        if ((!isCoinBase && !isCoinStake) || (confirmationCount > coinbaseMaturity))
                                        {
                                            UnspentOutputReferences.Add(new UnspentOutputReference
                                            {
                                                Account = MyWallet.hdAccount,
                                                Address = _Adr,
                                                Transaction = transactionData,
                                                Confirmations = confirmationCount.Value
                                            });
                                        }
                                    }


                                }
                            }
                        }


                    }
                    catch { }
                }

                foreach (var _Adr in MyWallet.hdAccount.ExternalAddresses)
                {
                    try
                    {
                        AddressBalance addressBalance = await new AddressManager().GetAddressBalance(_Adr.Bech32Address);
                        if (addressBalance != null)
                        {
                            if (addressBalance.balance > 0)
                            {
                                var _Trxs = await GetAddressTransectionT(_Adr.Bech32Address, new BlockCoreNetworks().GetNetwork(MyWallet.Network));
                                if (_Trxs != null)
                                {
                                    int countFrom = currentChainHeight + 1;

                                    foreach (TransactionOutputData transactionData in await UnspentTransactions(_Adr, network, _Trxs))
                                    {
                                        int? confirmationCount = 0;

                                        if (transactionData.BlockHeight != null)
                                        {
                                            confirmationCount = countFrom >= transactionData.BlockHeight ? countFrom - transactionData.BlockHeight : 0;
                                        }

                                        if (confirmationCount < confirmations)
                                        {
                                            continue;
                                        }

                                        bool isCoinBase = transactionData.IsCoinBase ?? false;
                                        bool isCoinStake = transactionData.IsCoinStake ?? false;

                                        // Check if this wallet is a normal purpose wallet (not cold staking, etc).
                                        //if (this.IsNormalAccount())
                                        //{
                                        //    bool isColdCoinStake = transactionData.IsColdCoinStake ?? false;

                                        //    // Skip listing the UTXO if this is a normal wallet, and the UTXO is marked as an cold coin stake.
                                        //    if (isColdCoinStake)
                                        //    {
                                        //        continue;
                                        //    }
                                        //}

                                        // This output can unconditionally be included in the results.
                                        // Or this output is a ColdStake, CoinBase or CoinStake and has reached maturity.
                                        if ((!isCoinBase && !isCoinStake) || (confirmationCount > coinbaseMaturity))
                                        {
                                            UnspentOutputReferences.Add(new UnspentOutputReference
                                            {
                                                Account = MyWallet.hdAccount,
                                                Address = _Adr,
                                                Transaction = transactionData,
                                                Confirmations = confirmationCount.Value
                                            });
                                        }
                                    }


                                }
                            }
                        }


                    }
                    catch { }
                }

            }
            catch { }

            return UnspentOutputReferences;
        }
        private async Task<IEnumerable<TransactionOutputData>> UnspentTransactions(HdAddress _SourceAddress, Network network, List<AdressTrx> AdressTrxes)
        {
            var _outlist = new List<TransactionOutputData>();

            try
            {


                foreach (var li in AdressTrxes)
                {
                    try
                    {
                        var Transaction = new TransactionOutputData();

                        Transaction.AccountIndex = 0;
                        Transaction.Address = _SourceAddress.Address;
                        Transaction.Amount =   Money.Parse( (li.value * 0.00000001).ToString());
                        Transaction.ScriptPubKey = new Script(Encoders.Hex.DecodeData(li.scriptHex)) ;
                        Transaction.BlockHeight = li.blockIndex;
                        Transaction.Id = new uint256(li.outpoint.transactionId);
                        Transaction.Index = li.outpoint.outputIndex;
                        Transaction.OutPoint = new OutPoint(new uint256(li.outpoint.transactionId), li.outpoint.outputIndex);
                        Transaction.Hex = null;
                        var _bf = await GetBlockinfo(li, network);
                        if (_bf != null)
                        {

                            Transaction.BlockHash = new uint256(_bf.blockHash);
                        }
                        //_resoultsss.blockInfo.First(lb => lb.blockIndex == li.blockIndex)

                        _outlist.Add(Transaction);

                    }
                    catch { }
                }
            }
            catch { }
            return _outlist;
        }
        private async Task<BlockInfo> GetBlockinfo(AdressTrx adressTrx, Network network)
        {
            try
            {
                String _Url = new BlockCoreNetworks().GetIndexerUrl(network) + @"api/query/block/index/";
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_Url + adressTrx.blockIndex.ToString()),
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    BlockInfo myDeserializedClass = JsonConvert.DeserializeObject<BlockInfo>(body.ToString());
                    return myDeserializedClass;
                }
            }
            catch { }
            return null;
        }
        private async Task<List<AdressTrx>> GetAddressTransectionT(String SgwitAddress, Network network)
        {
            try
            {
                String _Url = new BlockCoreNetworks().GetIndexerUrl(network) + @"api/query/address/";

                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_Url + SgwitAddress + @"/transactions/unspent?confirmations=1&offset=0&limit=20"),
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var myDeserializedClass = JsonConvert.DeserializeObject<List<AdressTrx>>(body.ToString());

                    return myDeserializedClass;
                }
            }
            catch { }
            return null;
        }


        public string GetTransectionHex(String _Destantion, String _ChangEAddress, String Amount, String _Password, WalletFile MyWallet, out TransactionPolicyError[] errors)
        {


            HdAddress _ChangeAddress;
            try
            {
                _ChangeAddress = MyWallet.hdAccount.InternalAddresses.First(li => li.Bech32Address == _ChangEAddress);
            }
            catch { _ChangeAddress = MyWallet.hdAccount.ExternalAddresses.First(li => li.Bech32Address == _ChangEAddress); }


            try
            {
                Network network = new BlockCoreNetworks().GetNetwork(MyWallet.Network);

                try
                {
                    NBitcoin.Key.Parse(MyWallet.EncryptedSeed, _Password, network);
                }
                catch (Exception ex)
                {
                    errors = new TransactionPolicyError[1];
                    errors[0] = new TransactionPolicyError("Password Error");

                    return "";
                }



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


                List<OutPoint> outPoints = new List<OutPoint>();
                foreach (var _coins in MyWallet.UnspentOutputReferences)
                {
                    OutPoint outpoint = new OutPoint(_coins.Transaction.Id, _coins.Transaction.Index);
                    outPoints.Add(outpoint);
                }

                var _sum = MyWallet.UnspentOutputReferences.Sum(li => li.Transaction.Amount);

                var recipients = new List<Recipient>();
                recipients.Add(new Recipient
                {
                    ScriptPubKey = BitcoinAddress.Create(_Destantion, network).ScriptPubKey,
                    Amount = Money.Parse(Amount)
                });

               TransactionBuilder txBuilder = new TransactionBuilder(network);
                var context = new TransactionBuildContext(network)
                {
                    AccountReference = new WalletAccountReference(MyWallet.Name, "account 0"),
                    MinConfirmations = 0,
                    Shuffle = true, // We shuffle transaction outputs by default as it's better for anonymity.
                    WalletPassword = _Password,
                    Recipients = recipients,
                    UseSegwitChangeAddress = recipients[0].ScriptPubKey.IsScriptType(ScriptType.Witness),
                    TransactionFee = Money.Parse("0.0001"),
                    SelectedInputs = outPoints,
                    ChangeAddress = _ChangeAddress,
                    FeeType = FeeType.Medium,
                    AllowOtherInputs = false,
                    OpReturnData = "",
                    OpReturnRawData = new byte[0],
                    // UnspentOutputs = MyWallet.UnspentOutputReferences,
                };



              //  context.TransactionBuilder.AddCoins(coinList);
            //    context.TransactionBuilder.SendFees(Money.Parse("0.0001"));


                Transaction tx = context.TransactionBuilder.BuildTransaction(false);
                
                var resTransaction = txBuilder.Verify(tx, out errors); //check fully signed

                if (resTransaction)
                {
                    var _TX = tx.GetHash().ToString();
                    var transactionHex = tx.ToHex();
                    return transactionHex;

                }
            }
            catch (Exception _ex) {
                errors = new TransactionPolicyError[2];
                errors[0] = new TransactionPolicyError(_ex.Message);
            }

            


            return "";

        }








    }
}
