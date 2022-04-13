using Blockcore.Configuration;
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
    public class BlockCoreNewTrx
    {
        private static readonly Money PretendMaxFee = Money.Coins(1);
        public Transaction CreateNewTransection(BlockCoreWallet _BlockCoreWallet, String _Destination, String _SourceAddress, HdAddress _ChangeAddress)
        {
            try
            {
                var _Network = GetNetwork(_BlockCoreWallet.NetworkName);

                var _changeAddress = BitcoinAddress.Create(_ChangeAddress.Bech32Address, _Network);

                var recipients = new List<Recipient>();


                recipients.Add(new Recipient
                {
                    ScriptPubKey = BitcoinAddress.Create(_Destination, _Network).ScriptPubKey,
                    Amount = Money.Parse("7")
                });



                var context = new TransactionBuildContext(_Network)
                {
                    AccountReference = new WalletAccountReference(_BlockCoreWallet.WalletName, "account 0"),
                    TransactionFee = Money.Parse("0.0001"),
                    MinConfirmations = 1,
                    Shuffle = true, // We shuffle transaction outputs by default as it's better for anonymity.
                    OpReturnData = "",
                    OpReturnRawData = new byte[] { },
                    OpReturnAmount = null,
                    WalletPassword = _BlockCoreWallet.Password,
                    SelectedInputs = null,
                    AllowOtherInputs = false,
                    Recipients = recipients,
                    ChangeAddress = _ChangeAddress,
                    UseSegwitChangeAddress = true,
                    FeeType = FeeType.Low
                };

            

                Transaction transactionResult = BuildTransaction(_BlockCoreWallet, _Destination, _SourceAddress, _ChangeAddress.Bech32Address, context);


                var Hex = transactionResult.ToHex();
                var Fee = context.TransactionFee;
                var TransactionId = transactionResult.GetHash();

                return transactionResult;
            }
            catch (Exception _ex) { }

            return null;
        }


        public async Task<double> GetWalletBalance(BlockCoreWallet _BlockCoreWallet)
        {
            try
            {
                double balance = 0;
                foreach (var _adr in _BlockCoreWallet.ReceivingAddresses)
                {
                    var _NewBalance = await GetAddressBalance(_adr.Bech32Address);
                    if(_NewBalance > 0 )
                    {
                        if (_BlockCoreWallet.AddressValues == null) _BlockCoreWallet.AddressValues = new List<AddressValue>();
                        AddressValue addressValue = new AddressValue();
                        addressValue.Address = _adr;
                        addressValue.value = _NewBalance;

                        _BlockCoreWallet.AddressValues.Add(addressValue);
                    }

                    balance += _NewBalance;
                }

                foreach (var _adr in _BlockCoreWallet.ChangeAddresses)
                {
                    var _NewBalance = await GetAddressBalance(_adr.Bech32Address);
                    if (_NewBalance > 0)
                    {
                        if (_BlockCoreWallet.AddressValues == null) _BlockCoreWallet.AddressValues = new List<AddressValue>();
                        AddressValue addressValue = new AddressValue();
                        addressValue.Address = _adr;
                        addressValue.value = _NewBalance;

                        _BlockCoreWallet.AddressValues.Add(addressValue);
                    }

                    balance += _NewBalance;
                }

                return balance;
            }
            catch { }
            return 0;
        }

        public async Task<Double> GetAddressBalance(String SgwitAddress)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://sbc.indexer.blockcore.net/api/query/address/" + SgwitAddress),
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    indexerAdress myDeserializedClass = JsonConvert.DeserializeObject<indexerAdress>(body.ToString());
                    return myDeserializedClass.balance * 0.00000001;
                }
            }
            catch { }
            return 0;
        }
        private Transaction BuildTransaction(BlockCoreWallet blockCoreWallet, string destination, string sourceAddress, string changeAddress, TransactionBuildContext context)
        {
            InitializeTransactionBuilder(blockCoreWallet, destination, sourceAddress, changeAddress, context);

            const int maxRetries = 5;
            int retryCount = 0;

            TransactionPolicyError[] errors = null;
            while (retryCount <= maxRetries)
            {
                if (context.Shuffle)
                    context.TransactionBuilder.Shuffle();

                Transaction transaction = context.TransactionBuilder.BuildTransaction(false);
                if (context.Sign)
                {
                    ICoin[] coinsSpent = context.TransactionBuilder.FindSpentCoins(transaction);
                    // TODO: Improve this as we already have secrets when running a retry iteration.
                    AddSecrets(context, coinsSpent, blockCoreWallet);
                    context.TransactionBuilder.SignTransactionInPlace(transaction);
                }

                if (context.TransactionBuilder.Verify(transaction, out errors))
                    return transaction;

                // Retry only if error is of type 'FeeTooLowPolicyError'
                if (!errors.Any(e => e is FeeTooLowPolicyError)) break;

                retryCount++;
            }

            string errorsMessage = string.Join(" - ", errors.Select(s => s.ToString()));
            Console.WriteLine($"Could not build the transaction. Details: {errorsMessage}");
            throw new($"Could not build the transaction. Details: {errorsMessage}");
        }

        private void AddSecrets(TransactionBuildContext context, ICoin[] coinsSpent, BlockCoreWallet blockCoreWallet)
        {
            var _Network = GetNetwork(blockCoreWallet.NetworkName);


            if (!context.Sign)
                return;


            ExtKey extendedKey = blockCoreWallet.mnemonic.DeriveExtKey(blockCoreWallet.Passphrase);



            var signingKeys = new HashSet<ISecret>();
            var added = new HashSet<HdAddress>();
            foreach (Coin coinSpent in coinsSpent)
            {
                //obtain the address relative to this coin (must be improved)
               // HdAddress address = context.UnspentOutputs.First(output => output.ToOutPoint() == coinSpent.Outpoint).Address;
                HdAddress address = context.UnspentOutputs.First().Address;
                if (added.Contains(address))
                    continue;

                ExtKey addressExtKey = blockCoreWallet.SeedExtKey.Derive(new KeyPath(address.HdPath));
                BitcoinExtKey addressPrivateKey = addressExtKey.GetWif(_Network);
                signingKeys.Add(addressPrivateKey);
                added.Add(address);
            }

            context.TransactionBuilder.AddKeys(signingKeys.ToArray());
        }

        private void InitializeTransactionBuilder(BlockCoreWallet blockCoreWallet, string destination, string sourceAddress, string changeAddress, TransactionBuildContext context)
        {
            context.TransactionBuilder.CoinSelector = new DefaultCoinSelector
            {
                GroupByScriptPubKey = false
            };
            context.TransactionBuilder.DustPrevention = false;

            if (context.SelectedInputs != null && context.SelectedInputs.Any())
            {
                context.TransactionBuilder.CoinSelector = new AllCoinsSelector();
            }

            AddRecipients(context);
            AddOpReturnOutput(context);
            AddCoins(context, blockCoreWallet);
            FindChangeAddress(context, blockCoreWallet, changeAddress);
            AddFee(context, blockCoreWallet);

            BitcoinAddress bitcoinAddress = BitcoinAddress.Create(destination, GetNetwork());
            BitcoinAddress bitcoinChangeAddress = BitcoinAddress.Create(changeAddress, GetNetwork());
            TxOut txOut = new TxOut(Money.Coins(5), bitcoinAddress.ScriptPubKey);
            TxIn _TxIn = new TxIn(bitcoinChangeAddress.ScriptPubKey);

            Transaction transaction1 = new Transaction();           
            transaction1.AddInput(_TxIn);
            OutPoint outPoint = new OutPoint(transaction1, 0);

            if (context.SelectedInputs == null)
                context.SelectedInputs = new List<OutPoint>();
            context.SelectedInputs.Add(outPoint);

        }

        private void AddFee(TransactionBuildContext context, BlockCoreWallet blockCoreWallet)
        {
            var _Network = GetNetwork(blockCoreWallet.NetworkName);
            Money fee;
            Money minTrxFee = new Money(_Network.MinTxFee, MoneyUnit.Satoshi);

            // If the fee hasn't been set manually, calculate it based on the fee type that was chosen.
            if (context.TransactionFee == null)
            {


                FeeRate feeRate = context.OverrideFeeRate ?? getWalletFeePolicy(_Network).GetFeeRate(context.FeeType.ToConfirmations());
                fee = context.TransactionBuilder.EstimateFees(feeRate);

                // Make sure that the fee is at least the minimum transaction fee.
                fee = Math.Max(fee, minTrxFee);
            }
            else
            {
                if (context.TransactionFee < minTrxFee)
                {
                    throw new($"Not enough fees. The minimun fee is {minTrxFee}.");
                }

                fee = context.TransactionFee;
            }

            context.TransactionBuilder.SendFees(fee);
            context.TransactionFee = fee;

        }

        private WalletFeePolicy getWalletFeePolicy(Network network)
        {

            NodeSettings nodeSettings = new NodeSettings(network);
            WalletFeePolicy walletFeePolicy = new WalletFeePolicy(nodeSettings);
            return walletFeePolicy;
        }

        private long ConvertToTimestamp(DateTime value)
        {
            TimeZoneInfo NYTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime NyTime = TimeZoneInfo.ConvertTime(value, NYTimeZone);
            TimeZone localZone = TimeZone.CurrentTimeZone;
            System.Globalization.DaylightTime dst = localZone.GetDaylightChanges(NyTime.Year);
            NyTime = NyTime.AddHours(-1);
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            TimeSpan span = (NyTime - epoch);
            return (long)Convert.ToDouble(span.TotalSeconds);
        }
        private void FindChangeAddress(TransactionBuildContext context, BlockCoreWallet blockCoreWallet, string changeAddress)
        {
            var _Network = GetNetwork(blockCoreWallet.NetworkName);

            if (context.ChangeAddress == null)
            {
                // If no change address is supplied, get a new address to send the change to.
                context.ChangeAddress = GetUnusedChangeAddress(blockCoreWallet, changeAddress);
            }

            if (context.UseSegwitChangeAddress)
            {
                context.TransactionBuilder.SetChange(new BitcoinWitPubKeyAddress(context.ChangeAddress.Bech32Address, _Network).ScriptPubKey);
            }
            else
            {
                context.TransactionBuilder.SetChange(context.ChangeAddress.ScriptPubKey);
            }
        }

        private HdAddress GetUnusedChangeAddress(BlockCoreWallet blockCoreWallet, String _ChangedAddress)
        {

            var res = blockCoreWallet.ChangeAddresses.FirstOrDefault(x => x.Bech32Address == _ChangedAddress);
            return res;
        }

     
        private void AddCoins(TransactionBuildContext context, BlockCoreWallet blockCoreWallet )
        {
            var _Network = GetNetwork(blockCoreWallet.NetworkName);


            context.UnspentOutputs = GetSpendableTransactionsInAccount(blockCoreWallet, context.AccountReference, context.MinConfirmations).ToList();

            if (context.UnspentOutputs.Count == 0)
            {
                throw new("No spendable transactions found.");
            }

            // Get total spendable balance in the account.
            long balance = context.UnspentOutputs.Sum(t => t.Transaction.Amount);
            long totalToSend = context.Recipients.Sum(s => s.Amount) + (context.OpReturnAmount ?? Money.Zero);
            if (balance < totalToSend)
                throw new("Not enough funds.");

            Money sum = 0;
            var coins = new List<Coin>();

            if (context.SelectedInputs != null && context.SelectedInputs.Any())
            {
                // 'SelectedInputs' are inputs that must be included in the
                // current transaction. At this point we check the given
                // input is part of the UTXO set and filter out UTXOs that are not
                // in the initial list if 'context.AllowOtherInputs' is false.

                Dictionary<OutPoint, UnspentOutputReference> availableHashList = context.UnspentOutputs.ToDictionary(item => item.ToOutPoint(), item => item);

                if (!context.SelectedInputs.All(input => availableHashList.ContainsKey(input)))
                    throw new("Not all the selected inputs were found on the wallet.");

                if (!context.AllowOtherInputs)
                {
                    foreach (KeyValuePair<OutPoint, UnspentOutputReference> unspentOutputsItem in availableHashList)
                    {
                        if (!context.SelectedInputs.Contains(unspentOutputsItem.Key))
                            context.UnspentOutputs.Remove(unspentOutputsItem.Value);
                    }
                }

                foreach (OutPoint outPoint in context.SelectedInputs)
                {
                    UnspentOutputReference item = availableHashList[outPoint];

                    coins.Add(new Coin(item.Transaction.Id, (uint)item.Transaction.Index, item.Transaction.Amount, item.Transaction.ScriptPubKey));
                    sum += item.Transaction.Amount;
                }
            }

            foreach (UnspentOutputReference item in context.UnspentOutputs
                .OrderByDescending(a => a.Confirmations > 0)
                .ThenByDescending(a => a.Transaction.Amount))
            {
                if (context.SelectedInputs?.Contains(item.ToOutPoint()) ?? false)
                    continue;

                // If the total value is above the target
                // then it's safe to stop adding UTXOs to the coin list.
                // The primary goal is to reduce the time it takes to build a trx
                // when the wallet is bloated with UTXOs.

                // Get to our total, and then check that we're a little bit over to account for tx fees.
                // If it gets over totalToSend but doesn't hit this break, that's fine too.
                // The TransactionBuilder will have a go with what we give it, and throw NotEnoughFundsException accurately if it needs to.
                if (sum > totalToSend + PretendMaxFee)
                    break;

                coins.Add(new Coin(item.Transaction.Id, (uint)item.Transaction.Index, item.Transaction.Amount, item.Transaction.ScriptPubKey));
                sum += item.Transaction.Amount;
            }

            // All the UTXOs are added to the builder without filtering.
            // The builder then has its own coin selection mechanism
            // to select the best UTXO set for the corresponding amount.
            // To add a custom implementation of a coin selection override
            // the builder using builder.SetCoinSelection().

            // var _TaretAddress = BitcoinAddress.Create(_targetAddress, _Network);

            // context.TransactionBuilder.AddCoins(AddCoin(_Network, _TaretAddress));

            context.TransactionBuilder.AddCoins(coins);
        }



        private static ICoin[] AddCoin(Blockcore.Networks.Network _network, BitcoinAddress _TaretAddress)
        {
            TransactionBuilder builder = new TransactionBuilder(_network);
            var transaction = _network.CreateTransaction();
            transaction.Outputs.Add(new TxOut(Money.Coins(10), _TaretAddress.ScriptPubKey));
        //    transaction.Inputs.Add(new TxIn(Money.));
            Coin[] coins = transaction.Outputs.AsCoins().ToArray();
            return coins;
        }


        private IEnumerable<UnspentOutputReference> GetSpendableTransactionsInAccount(BlockCoreWallet blockCoreWallet, WalletAccountReference walletAccountReference, int confirmations = 0)
        {
            var _Network = GetNetwork(blockCoreWallet.NetworkName);

            UnspentOutputReference[] res = null;

            HdAccount account = blockCoreWallet.HdAccount;

            if (account == null)
            {

                throw new($"Account '{walletAccountReference.AccountName}' in wallet '{walletAccountReference.WalletName}' not found.");
            }

            res = GetSpendableTransactions(blockCoreWallet, this.chainHeight, _Network.Consensus.CoinbaseMaturity, confirmations).ToArray();


            return res;
        }
        private int chainHeight = 127632;
      

        private IEnumerable<UnspentOutputReference> GetSpendableTransactions(BlockCoreWallet blockCoreWallet, int currentChainHeight, long coinbaseMaturity, int confirmations)
        {

            var _data1 = this.GetCombinedAddress(blockCoreWallet);
            foreach (HdAddress address in this.GetCombinedAddress(blockCoreWallet))
            {
                // A block that is at the tip has 1 confirmation.
                // When calculating the confirmations the tip must be advanced by one.

                int countFrom = currentChainHeight + 1;
                foreach (TransactionOutputData transactionData in UnspentTransactions(address, blockCoreWallet, currentChainHeight))
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
                        yield return new UnspentOutputReference
                        {
                            Account = blockCoreWallet.HdAccount,                           
                            Address = address,
                            Transaction = transactionData,
                            Confirmations = confirmationCount.Value
                        };
                    }
                }
            }
        }

        private     IEnumerable<TransactionOutputData> UnspentTransactions(HdAddress _SourceAddress, BlockCoreWallet blockCoreWallet, int currentChainHeight)
        {
            var _outlist = new List<TransactionOutputData>();

            try
            {



                var _resoultsss = blockCoreWallet.AdressDatas.First(li => li.HdAddress.Bech32Address == _SourceAddress.Bech32Address);
                var _Network = GetNetwork(blockCoreWallet.NetworkName);

                foreach (var li in _resoultsss.AdressTrxes)
                {
                    var Transaction = new TransactionOutputData();
                    Transaction.Address = _SourceAddress.Address;
                    Transaction.Amount = new Money(li.value);
                    Transaction.ScriptPubKey = _SourceAddress.ScriptPubKey;
                    Transaction.BlockHeight = li.blockIndex;
                    Transaction.Id = new uint256(li.transactionHash);
                    Transaction.OutPoint = new OutPoint(new uint256(li.transactionHash),0);
                    var _bf = _resoultsss.blockInfo.First(lb => lb.blockIndex == li.blockIndex);

               
                    Transaction.BlockHash = new uint256(_bf.blockHash);
                    _outlist.Add(Transaction);
                }
            }
            catch { }
            return _outlist;
        }


        public async Task<List<AdressTrx>> GetAddressTransectionT(String SgwitAddress)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(@"https://sbc.indexer.blockcore.net/api/query/address/"+ SgwitAddress + @"/transactions?offset=0&limit=50" ),
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
            return new List<AdressTrx>();
        }

        public async Task<BlockInfo> GetAddressTransectionTindex( AdressTrx adressTrx )
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(@"https://sbc.indexer.blockcore.net/api/query/block/index/" + adressTrx.blockIndex.ToString()),
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
            return new BlockInfo();
        }

        public async Task  SyncAdressTransection(BlockCoreWallet _WalletObject  , HdAddress SgwitAddress)
        {
            var _Trx = await GetAddressTransectionT(SgwitAddress.Bech32Address);
            if (_WalletObject.AdressDatas == null)
            {
                _WalletObject.AdressDatas = new List<AdressData>();
            }

            AdressData adressData = new AdressData();
            adressData.AdressTrxes = new List<AdressTrx>();
            adressData.AdressTrxes.AddRange(_Trx);
            adressData.HdAddress = new HdAddress();
            adressData.HdAddress = SgwitAddress;

            adressData.blockInfo = new List<BlockInfo>();
            foreach (var adress in adressData.AdressTrxes)
            {
                adressData.blockInfo.Add(await GetAddressTransectionTindex(adress));
            }



            _WalletObject.AdressDatas.Add(adressData);

        }


        public IEnumerable<HdAddress> GetCombinedAddress(BlockCoreWallet blockCoreWallet)
        {
            IEnumerable<HdAddress> addresses = new List<HdAddress>();

            addresses = blockCoreWallet.ChangeAddresses; ;



            addresses = addresses.Concat(blockCoreWallet.ReceivingAddresses);
          

            return addresses;
        }

        private void AddOpReturnOutput(TransactionBuildContext context)
        {
            byte[] bytes = context.OpReturnRawData ?? Encoding.UTF8.GetBytes(context.OpReturnData);

            // TODO: Get the template from the network standard scripts instead
            Script opReturnScript = TxNullDataTemplate.Instance.GenerateScriptPubKey(bytes);
            context.TransactionBuilder.Send(opReturnScript, context.OpReturnAmount ?? Money.Zero);
        }

        private void AddRecipients(TransactionBuildContext context)
        {
            foreach (Recipient recipient in context.Recipients)
                context.TransactionBuilder.Send(recipient.ScriptPubKey, recipient.Amount);


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
