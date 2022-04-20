
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

        private IEnumerable<HdAddress> CreateAddresses(HdAccount hdAccount, Network network, int addressesQuantity = 20, bool isChange = false)
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
                PubKey pubkey = GeneratePublicKey(hdAccount.ExtendedPubKey, i, isChange, network);

                // Generate the P2PKH address corresponding to the pubkey.
                BitcoinPubKeyAddress address = pubkey.GetAddress(network);
                BitcoinWitPubKeyAddress witAddress = pubkey.GetSegwitAddress(network);

                // Add the new address details to the list of addresses.
                var newAddress = new HdAddress
                {
                    Index = i,
                    HdPath = CreateHdPath(CoinType(network.CoinTicker), 0, isChange, i),
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




        private void GetAllAccountAndAdress(WalletFile _WalletObject, String Password, String mnemonic, String Passphrase)
        {
            String newAccountName = AccountName;
            string accountHdPath = GetAccountHdPath(CoinType(_WalletObject.Network), 0);


            ExtKey extendedKey = new Mnemonic(mnemonic).DeriveExtKey(Passphrase);
            var privateKey = extendedKey.PrivateKey;
            var chainCode = extendedKey.ChainCode;
            string encryptedSeed = extendedKey.PrivateKey.GetEncryptedBitcoinSecret(Password, GetNetwork(_WalletObject.Network)).ToWif();

            var PrivateKey = DecryptSeed(encryptedSeed, Password, GetNetwork(_WalletObject.Network));
            var accountExtPubKey = GetExtendedPublicKey(PrivateKey, extendedKey.ChainCode, accountHdPath);




            var _network = GetNetwork(_WalletObject.Network);

            HdAccount _HdAccount = new HdAccount
            {
                Index = 0,
                ExtendedPubKey = accountExtPubKey.ToString(GetNetwork(_WalletObject.Network)),
                ExternalAddresses = new List<HdAddress>(),
                InternalAddresses = new List<HdAddress>(),
                Name = newAccountName,
                HdPath = accountHdPath,
                CreationTime = DateTimeOffset.UtcNow
            };

            _WalletObject.hdAccount = _HdAccount;


            // unused
            //_WalletObject.hdAccount.ExternalAddresses.AddRange();
            CreateAddresses(_HdAccount, _network, 20);
            CreateAddresses(_HdAccount, _network, 20, true);

            //changed
            //   _WalletObject.hdAccount.InternalAddresses.AddRange(CreateAddresses(_HdAccount, _WalletObject.accountExtPubKey.ToString(GetNetwork(_WalletObject.NetworkName)), GetNetwork(_WalletObject.NetworkName), _WalletObject, 20, true));

        }

        public static String WalletDirectory = Directory.GetCurrentDirectory() + @"\Wallets\";

        public Boolean Save(WalletFile walletFile)
        {


            if (Directory.Exists(WalletDirectory) == false)
            {
                Directory.CreateDirectory(WalletDirectory);
            }

            String walletFilePath = WalletDirectory + walletFile.Name + ".wallet.json";

            if (File.Exists(walletFilePath))
                return false;


            try
            {

                var directoryPath = Path.GetDirectoryName(Path.GetFullPath(walletFilePath));
                if (directoryPath != null) Directory.CreateDirectory(directoryPath);

                return WalletFileSerializer.Serialize(walletFile);


            }
            catch { }
            return false;
        }


    
        public Boolean CreateNewWallet(NewWalletRequst _NewWalletRequst)
        {
            if (Directory.Exists(WalletDirectory) == false)
            {
                Directory.CreateDirectory(WalletDirectory);
            }

            String walletFilePath = WalletDirectory + _NewWalletRequst.WalletName + ".wallet.json";

            if (File.Exists(walletFilePath))
                return false;


            try
            {


                ExtKey extendedKey = new Mnemonic(_NewWalletRequst.mnemonic).DeriveExtKey(_NewWalletRequst.Passphrase);
                var privateKey = extendedKey.PrivateKey;
                var chainCode = extendedKey.ChainCode;
                string encryptedSeed = extendedKey.PrivateKey.GetEncryptedBitcoinSecret(_NewWalletRequst.Password, GetNetwork(_NewWalletRequst.NetworkName)).ToWif();
                string accountHdPath = GetAccountHdPath(CoinType(_NewWalletRequst.NetworkName), 0);
                var PrivateKey = DecryptSeed(encryptedSeed, _NewWalletRequst.Password, GetNetwork(_NewWalletRequst.NetworkName));
                var accountExtPubKey = GetExtendedPublicKey(PrivateKey, extendedKey.ChainCode, accountHdPath);


                WalletFile walletFile = new WalletFile();
                walletFile.Name = _NewWalletRequst.WalletName;
                walletFile.Network = _NewWalletRequst.NetworkName;
                walletFile.EncryptedSeed = encryptedSeed;
                walletFile.coinType = CoinType(_NewWalletRequst.NetworkName);
                var chainCodeString = Convert.ToBase64String(chainCode);
                walletFile.ChainCode = chainCodeString;
                walletFile.ConfirmedAmount = 0;
                walletFile.UnConfirmedAmount = 0;
                walletFile.UnspentOutputReferences = new List<UnspentOutputReference>();

                walletFile.CreationTime = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                GetAllAccountAndAdress(walletFile, _NewWalletRequst.Password, _NewWalletRequst.mnemonic, _NewWalletRequst.Passphrase);

                return Save(walletFile);
            }
            catch { }
            return false;
        }


        public List<WalletFile> GetAllWalletInStore()
        {
            List<WalletFile> _wallets = new List<WalletFile>();
            try
            {
                if (Directory.Exists(Directory.GetCurrentDirectory() + @"\Wallets\") == true)
                {
                    String _Path = Directory.GetCurrentDirectory() + @"\Wallets\";

                    foreach (var _walletFile in Directory.GetFiles(_Path, "*.json"))
                    {
                        try
                        {
                            WalletFile _Temp = LoadWallet(_walletFile);
                            _wallets.Add(_Temp);
                        }
                        catch { }
                    }

                }
            }
            catch { }

            return _wallets;
        }

        public WalletFile LoadWallet(String walletFilePath)
        {

            if (File.Exists(walletFilePath) == false)
                return new WalletFile();
            try
            {

                WalletFile _WalletFile = WalletFileSerializer.Deserialize(walletFilePath);

                return _WalletFile;
            }
            catch { }
            return new WalletFile();
        }

        public int CoinType(string networkName)
        {
            return new BlockCoreNetworks().CoinType(GetNetwork(networkName));
        }

        public Network GetNetwork(string network)
        {
            return new BlockCoreNetworks().GetAllNetworks().First(li => li.CoinTicker == network);
        }

        public Boolean RecoverWallet(NewWalletRequst _WalletObject)
        {
            return CreateNewWallet(_WalletObject);
        }


    }
}
