
using Blockcore.Configuration;
using Blockcore.Connection.Broadcasting;
using Blockcore.Consensus.ScriptInfo;
using Blockcore.Consensus.TransactionInfo;
using Blockcore.Features.Wallet;
using Blockcore.Features.Wallet.Api.Models;
using Blockcore.Features.Wallet.Database;
using Blockcore.Features.Wallet.Types;
using Blockcore.Networks;
using Microsoft.Extensions.Logging;
using NBitcoin;
using NBitcoin.DataEncoders;
using NBitcoin.Policy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallet.core
{
    public class WalletManager
    {
        public const String AccountName = "account 0";


        public static Mnemonic GenerateMnemonic(string language = "English", int wordCount = 12)
        {
            Wordlist wordList;
            switch (language.ToLowerInvariant())
            {
                case "english":
                    wordList = Wordlist.English;
                    break;

                case "french":
                    wordList = Wordlist.French;
                    break;

                case "spanish":
                    wordList = Wordlist.Spanish;
                    break;

                case "japanese":
                    wordList = Wordlist.Japanese;
                    break;

                case "chinesetraditional":
                    wordList = Wordlist.ChineseTraditional;
                    break;

                case "chinesesimplified":
                    wordList = Wordlist.ChineseSimplified;
                    break;

                default:
                    throw new FormatException($"Invalid language '{language}'. Choices are: English, French, Spanish, Japanese, ChineseSimplified and ChineseTraditional.");
            }

            var count = (WordCount)wordCount;

            // generate the mnemonic
            var mnemonic = new Mnemonic(wordList, count);

            return mnemonic;
        }


        public async Task<Double> GetWalletBalance(BlockCoreWallet _BlockCoreWallet)
        {
            try
            {
                Double balance = 0;
                foreach (var _adr in _BlockCoreWallet.ReceivingAddresses)
                {
                    balance += await GetAddressBalance(_adr.Bech32Address);
                }

                foreach (var _adr in _BlockCoreWallet.ChangeAddresses)
                {
                    balance += await GetAddressBalance(_adr.Bech32Address);
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

        private string GetAccountHdPath(int coinType, int accountIndex)
        {
            return $"m/44'/{coinType}'/{accountIndex}'";
        }

        private Key DecryptSeed(string encryptedSeed, string password, Network network)
        {
            return Key.Parse(encryptedSeed, password, network);
        }

        private ExtPubKey GetExtendedPublicKey(Key privateKey, byte[] chainCode, string hdPath)
        {

            // get extended private key
            var seedExtKey = new ExtKey(privateKey, chainCode);
            ExtKey addressExtKey = seedExtKey.Derive(new KeyPath(hdPath));
            ExtPubKey extPubKey = addressExtKey.Neuter();
            return extPubKey;
        }
        private string CreateHdPath(int coinType, int accountIndex, bool isChange, int addressIndex)
        {
            int change = isChange ? 1 : 0;
            return $"m/44'/{coinType}'/{accountIndex}'/{change}/{addressIndex}";
        }

        private static PubKey GeneratePublicKey(string accountExtPubKey, int index, bool isChange, Network network)
        {
            //var _Base58 = Base58Encoding.Decode(accountExtPubKey);
            //var actualText = Base58Encoding.Encode(_Base58);

            int change = isChange ? 1 : 0;
            var keyPath = new KeyPath($"{change}/{index}");
            // TODO: Should probably explicitly be passing the network into Parse
            ExtPubKey extPubKey = ExtPubKey.Parse(accountExtPubKey, network).Derive(keyPath);
            return extPubKey.PubKey;
        }

        private IEnumerable<HdAddress> CreateAddresses(HdAccount hdAccount, String ExtendedPubKey, Network network, BlockCoreWallet _BlockCoreWallet, int addressesQuantity = 20, bool isChange = false)
        {
            ICollection<HdAddress> addresses = isChange ? hdAccount.InternalAddresses : hdAccount.ExternalAddresses;



            // Get the index of the last address.
            int firstNewAddressIndex = 0;
            if (addresses.Any())
            {
                firstNewAddressIndex = addresses.Max(add => add.Index) + 1;
            }

            var addressesCreated = new List<HdAddress>();
            for (int i = firstNewAddressIndex; i < firstNewAddressIndex + addressesQuantity; i++)
            {
                // Retrieve the pubkey associated with the private key of this address index.
                PubKey pubkey = GeneratePublicKey(ExtendedPubKey, i, isChange, network);

                // Generate the P2PKH address corresponding to the pubkey.
                BitcoinPubKeyAddress address = pubkey.GetAddress(network);
                BitcoinWitPubKeyAddress witAddress = pubkey.GetSegwitAddress(network);

                // Add the new address details to the list of addresses.
                var newAddress = new HdAddress
                {
                    Index = i,
                    HdPath = CreateHdPath(CoinType(_BlockCoreWallet.NetworkName), 0, isChange, i),
                    ScriptPubKey = address.ScriptPubKey,
                    Pubkey = pubkey.ScriptPubKey,
                    Bech32Address = witAddress.ToString(),
                    Address = address.ToString()
                };


                addresses.Add(newAddress);
                addressesCreated.Add(newAddress);
            }


            if (isChange)
            {
                hdAccount.InternalAddresses = addresses;
            }
            else
            {
                hdAccount.ExternalAddresses = addresses;
            }

            return addressesCreated;
        }



        public Boolean CreateNewWallet(BlockCoreWallet _WalletObject)
        {         
            String walletFilePath = Directory.GetCurrentDirectory() + @"\Wallets\"+ _WalletObject.WalletName + ".wallet.json";

            if (File.Exists(walletFilePath))
                return false;

            try
            {

                _WalletObject.CreationTime = DateTimeOffset.UtcNow;
                ExtKey extendedKey = _WalletObject.mnemonic.DeriveExtKey(_WalletObject.Passphrase);
                _WalletObject.SeedExtKey = extendedKey;
                string encryptedSeed = extendedKey.PrivateKey.GetEncryptedBitcoinSecret(_WalletObject.Password, GetNetwork(_WalletObject.NetworkName)).ToWif();

                string accountHdPath = GetAccountHdPath(CoinType(_WalletObject.NetworkName), 0);
                _WalletObject.PrivateKey = DecryptSeed(encryptedSeed, _WalletObject.Password, GetNetwork(_WalletObject.NetworkName));
                _WalletObject.accountExtPubKey = GetExtendedPublicKey(_WalletObject.PrivateKey, extendedKey.ChainCode, accountHdPath);


                _WalletObject.ReceivingAddresses = new List<HdAddress>();
                _WalletObject.ChangeAddresses = new List<HdAddress>();

                GetAllAccountAndAdress(_WalletObject);

                return Save(_WalletObject);
            }
            catch { }
            return false;
        }

        private void GetAllAccountAndAdress(BlockCoreWallet _WalletObject)
        {
            String newAccountName = AccountName;
            string accountHdPath = GetAccountHdPath(CoinType(_WalletObject.NetworkName), 0);

            HdAccount _HdAccount = new HdAccount
            {
                Index = 0,
                ExtendedPubKey = _WalletObject.accountExtPubKey.ToString(GetNetwork(_WalletObject.NetworkName)),
                ExternalAddresses = new List<HdAddress>(),
                InternalAddresses = new List<HdAddress>(),
                Name = newAccountName,
                HdPath = accountHdPath,
                CreationTime = _WalletObject.CreationTime
            };

            _WalletObject.HdAccount = _HdAccount;

            _WalletObject.ReceivingAddresses = new List<HdAddress>();
            _WalletObject.ChangeAddresses = new List<HdAddress>();
            // unused
            _WalletObject.ReceivingAddresses.AddRange(CreateAddresses(_HdAccount, _WalletObject.accountExtPubKey.ToString(GetNetwork(_WalletObject.NetworkName)), GetNetwork(_WalletObject.NetworkName), _WalletObject));

            //changed
            _WalletObject.ChangeAddresses.AddRange(CreateAddresses(_HdAccount, _WalletObject.accountExtPubKey.ToString(GetNetwork(_WalletObject.NetworkName)), GetNetwork(_WalletObject.NetworkName), _WalletObject, 20, true));

        }

        public Boolean Save(BlockCoreWallet _BlockCoreWallet)
        {
            var directoryPath = Directory.GetCurrentDirectory() + @"\Wallets\";
            String walletFilePath = directoryPath + _BlockCoreWallet.WalletName + ".wallet.json";
            if (File.Exists(walletFilePath))
                return false;

            try
            {

                if (!Directory.Exists( directoryPath)) Directory.CreateDirectory(directoryPath);

                var privateKey = _BlockCoreWallet.SeedExtKey.PrivateKey;
                var chainCode = _BlockCoreWallet.SeedExtKey.ChainCode;

                var encryptedBitcoinPrivateKeyString = privateKey.GetEncryptedBitcoinSecret(_BlockCoreWallet.Password, GetNetwork(_BlockCoreWallet.NetworkName)).ToWif();
                var chainCodeString = Convert.ToBase64String(chainCode);

                var networkString = GetNetwork(_BlockCoreWallet.NetworkName).ToString();

                var creationTimeString = _BlockCoreWallet.CreationTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                WalletFileSerializer.Serialize(
                    walletFilePath,
                    encryptedBitcoinPrivateKeyString,
                    chainCodeString,
                    networkString,
                    creationTimeString);

                return true;
            }
            catch { }
            return false;
        }

        public BlockCoreWallet LoadWallet(BlockCoreWallet _WalletObject)
        {
            var directoryPath = Directory.GetCurrentDirectory() + @"\Wallets\";
            String walletFilePath = directoryPath + _WalletObject.WalletName + ".wallet.json";
            if (!File.Exists(walletFilePath))
                return null;
            try
            {

                var walletFileRawContent = WalletFileSerializer.Deserialize(walletFilePath);
                var encryptedBitcoinPrivateKeyString = walletFileRawContent.EncryptedSeed;
                var chainCodeString = walletFileRawContent.ChainCode;
                var chainCode = Convert.FromBase64String(chainCodeString);
                var networkString = walletFileRawContent.Network;
                DateTimeOffset creationTime = DateTimeOffset.ParseExact(walletFileRawContent.CreationTime, "yyyy-MM-dd", CultureInfo.InvariantCulture);


                BlockCoreWallet _BlockCoreWallet = new BlockCoreWallet();
                _BlockCoreWallet.WalletName = _WalletObject.WalletName;
                _BlockCoreWallet.Password = _WalletObject.Password;
                _BlockCoreWallet.CreationTime = creationTime;
                _BlockCoreWallet.NetworkName = walletFileRawContent.Network;
                _BlockCoreWallet.PrivateKey = Key.Parse(encryptedBitcoinPrivateKeyString, _WalletObject.Password, GetNetwork(walletFileRawContent.Network));
                _BlockCoreWallet.SeedExtKey = new ExtKey(_BlockCoreWallet.PrivateKey, chainCode);
                _BlockCoreWallet.mnemonic = _WalletObject.mnemonic;
                string accountHdPath = GetAccountHdPath(CoinType(_BlockCoreWallet.NetworkName), 0);
                _BlockCoreWallet.accountExtPubKey = GetExtendedPublicKey(_BlockCoreWallet.PrivateKey, _BlockCoreWallet.SeedExtKey.ChainCode, accountHdPath);


                GetAllAccountAndAdress(_BlockCoreWallet);


                return _BlockCoreWallet;
            }
            catch { }
            return new BlockCoreWallet();
        }


        public void RecoverWallet(BlockCoreWallet _WalletObject)
        {
            CreateNewWallet(_WalletObject);
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
