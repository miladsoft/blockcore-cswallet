using Blockcore.Consensus.ScriptInfo;
using Blockcore.Consensus.TransactionInfo;
using Blockcore.Features.Wallet;
using Blockcore.Features.Wallet.Database;
using Blockcore.Features.Wallet.Types;
using Blockcore.Networks;
using NBitcoin;
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


        public async Task<List<UnspentOutputReference>> GetSpendableTransactions(WalletFile MyWallet)
        {
            List<UnspentOutputReference> UnspentOutputReferences = new List<UnspentOutputReference>();
            try
            {
                Network network = new BlockCoreNetworks().GetNetwork(MyWallet.Network);
                int  coinbaseMaturity = (int)network.Consensus.CoinbaseMaturity;

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
        private async Task<IEnumerable<TransactionOutputData>> UnspentTransactions(HdAddress _SourceAddress, Network network , List<AdressTrx> AdressTrxes)
        {
            var _outlist = new List<TransactionOutputData>();

            try
            {

 
                foreach (var li in AdressTrxes)
                {
                    var Transaction = new TransactionOutputData();
                    Transaction.Address = _SourceAddress.Address;
                    Transaction.Amount = new Money(li.value);
                    Transaction.ScriptPubKey = _SourceAddress.ScriptPubKey;
                    Transaction.BlockHeight = li.blockIndex;
                    Transaction.Id = new uint256(li.transactionHash);
                    Transaction.OutPoint = new OutPoint(new uint256(li.transactionHash), 0);

                    var _bf = await GetBlockinfo(li, network);
                    if(_bf != null)
                    {

                        Transaction.BlockHash = new uint256(_bf.blockHash);
                    }
                    //_resoultsss.blockInfo.First(lb => lb.blockIndex == li.blockIndex)

                    _outlist.Add(Transaction);
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
                    RequestUri = new Uri(_Url + SgwitAddress + @"/transactions?offset=0&limit=50"),
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


        public string GetTransectionHex(String _Destantion , int Amount , String _Password ,WalletFile MyWallet)
        {
            var changeAddress = MyWallet.hdAccount.InternalAddresses.FirstOrDefault();
            try
            {
                Network network = new BlockCoreNetworks().GetNetwork(MyWallet.Network);


                try
                {
                    NBitcoin.Key.Parse(MyWallet.EncryptedSeed, _Password, network);
                }
                catch (Exception ex)
                {

                    return null;
                }

                var recipients = new List<Recipient>();
                recipients.Add(new Recipient
                {
                    ScriptPubKey = BitcoinAddress.Create(_Destantion, network).ScriptPubKey,
                    Amount =  Money.Parse(Amount.ToString())
                });

                TransactionBuilder txBuilder = new TransactionBuilder(network);
                var context = new TransactionBuildContext(network)
                {
                    AccountReference = new WalletAccountReference(network.Name,"account 0"),
                    Shuffle = true, // We shuffle transaction outputs by default as it's better for anonymity.
                    WalletPassword = _Password,
                    Recipients = recipients,
                    UseSegwitChangeAddress = recipients[0].ScriptPubKey.IsScriptType(ScriptType.Witness),
                    TransactionFee = Money.Parse("0.0001")
                };


                Transaction tx = context.TransactionBuilder.BuildTransaction(false);

                      TransactionPolicyError[] errors = null;
                var resTransaction = txBuilder.Verify(tx, out errors); //check fully signed


                if (resTransaction)
                {
                    var _TX = tx.GetHash().ToString();
                    var transactionHex = tx.ToHex();
                    return transactionHex;

                }
            }
            catch (Exception _ex) { }

            return "";

        }


    

     



    }
}
