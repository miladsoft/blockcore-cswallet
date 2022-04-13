using Blockcore.Networks;
using NBitcoin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace wallet.core
{
	public class WalletManager1
	{

		public   async Task GetWalletBalance(WalletManager1 safe)
        {




			List<BalanceOperation> _ListAllAdress = new List<BalanceOperation>();
			////_ListAllAdress = QueryOperationsPerSafeAddresses(safe, 7, HdPathType.Receive);
			//_ListAllAdress.AddRange(QueryOperationsPerSafeAddresses(safe, 20, HdPathType.Change));

			//Console.WriteLine("Change");
			//foreach (BalanceOperation _OP in _ListAllAdress)
			//{
			//	Console.WriteLine(_OP.SgwitAddress);
			//}

			_ListAllAdress = new List<BalanceOperation>();
			_ListAllAdress.AddRange(QueryOperationsPerSafeAddresses(safe, 20, HdPathType.Receive));
			Console.WriteLine("Receive");
			foreach (BalanceOperation _OP in _ListAllAdress)
			{
				Console.WriteLine(_OP.SgwitAddress);
			}

			Double _money = 0;
			foreach (BalanceOperation _OP in _ListAllAdress)
            {
				try
				{
					_money +=  await GetAddressMoney(_OP.SgwitAddress);
				}
				catch { }
            }

		}

        private List<BalanceOperation> QueryOperationsPerSafeAddresses(WalletManager1 safe, int minUnusedKeys , HdPathType _hdPathType )
        {
			 
			var addresses = GetFirstNAddresses(minUnusedKeys, _hdPathType,  new SafeAccount(0));
 

			List<BalanceOperation> operations = new List<BalanceOperation>();	

			foreach (var address in addresses)
            {
				BalanceOperation _op = new BalanceOperation();
				_op.BitcoinAddress = address;
				_op.SgwitAddress = address.ScriptPubKey.GetWitScriptAddress(address.Network).ToString();
				_op.MoneyAddress = 0;
				operations.Add(_op);
			}

			return operations;

		}

		private async Task<Double>  GetAddressMoney(String SgwitAddress)
        {
			try
			{
				var client = new HttpClient();
				var request = new HttpRequestMessage
				{
					Method = HttpMethod.Get,
					RequestUri = new Uri("https://sbc.indexer.blockcore.net/api/query/address/"+ SgwitAddress), 
				};
				using (var response = await client.SendAsync(request))
				{
					response.EnsureSuccessStatusCode();
					var body = await response.Content.ReadAsStringAsync();
					indexerAdress myDeserializedClass = JsonConvert.DeserializeObject<indexerAdress>(body.ToString ());
					return myDeserializedClass.balance * 0.00000001;
				}

				

			}
			catch { }
			return 0;
        }
		 

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

		Network network = Blockcore.Networks.SeniorBlockCoin.Networks.Networks.SeniorBlockCoin.Mainnet();

		public static DateTimeOffset EarliestPossibleCreationTime { get; set; } = DateTimeOffset.ParseExact("2017-02-19", "yyyy-MM-dd", CultureInfo.InvariantCulture);

		public DateTimeOffset CreationTime { get; }

		public ExtKey ExtKey { get; private set; }
		public BitcoinExtKey BitcoinExtKey => ExtKey.GetWif(network);
		public BitcoinExtPubKey GetBitcoinExtPubKey(int? index = null, HdPathType hdPathType = HdPathType.Receive, SafeAccount account = null) => GetBitcoinExtKey(index, hdPathType, account).Neuter();

		public BitcoinAddress GetAddress(int index, HdPathType hdPathType = HdPathType.Receive, SafeAccount account = null)
		{
	 

			return GetBitcoinExtKey(index, hdPathType, account).ScriptPubKey.GetDestinationAddress(network);
		}

		public IList<BitcoinAddress> GetFirstNAddresses(int addressCount, HdPathType hdPathType = HdPathType.Receive, SafeAccount account = null)
		{
			var addresses = new List<BitcoinAddress>();

			for (var i = 0; i < addressCount; i++)
			{
				addresses.Add(GetAddress(i, hdPathType, account));
			}

			return addresses;
		}

		// Let's generate a unique id from seedpublickey
		// Let's get the pubkey, so the chaincode is lost
		// Let's get the address, you can't directly access it from the safe
		// Also nobody would ever use this address for anythin
		/// <summary> If the wallet only differs by CreationTime, the UniqueId will be the same </summary>
		public string UniqueId => BitcoinExtKey.Neuter().ExtPubKey.PubKey.GetAddress(network).ToString();

		public string WalletFilePath { get; }

		protected WalletManager1(string password, string walletFilePath,   DateTimeOffset creationTime, Mnemonic mnemonic = null)
		{
			 
			WalletFilePath = walletFilePath;
			CreationTime = creationTime > EarliestPossibleCreationTime ? creationTime : EarliestPossibleCreationTime;

			if (mnemonic != null)
			{
				SetSeed(password, mnemonic);
			}

		}

		public WalletManager1(WalletManager1 safe)
		{
			 
			CreationTime = safe.CreationTime;
			ExtKey = safe.ExtKey;
			WalletFilePath = safe.WalletFilePath;
		}

		/// <summary>
		///     Creates a mnemonic, a seed, encrypts it and stores in the specified path.
		/// </summary>
		/// <param name="mnemonic">empty</param>
		/// <param name="password"></param>
		/// <param name="walletFilePath"></param>
		/// <param name="network"></param>
		/// <returns>Safe</returns>
		public static WalletManager1 Create(  Mnemonic mnemonic, string password, string walletFilePath )
		{
			var creationTime = new DateTimeOffset(DateTimeOffset.UtcNow.Date);

			var safe = new WalletManager1(password, walletFilePath,  creationTime);

			mnemonic = safe.SetSeed(password, mnemonic);

			safe.Save(password, walletFilePath, creationTime);

			return safe;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="mnemonic"></param>
		/// <param name="password"></param>
		/// <param name="walletFilePath"></param>
		/// <param name="network"></param>
		/// <param name="creationTime">if null then will default to EarliestPossibleCreationTime</param>
		/// <returns></returns>
		public static WalletManager1 Recover(Mnemonic mnemonic, string password, string walletFilePath,  DateTimeOffset? creationTime = null)
		{
			if (creationTime == null)
				creationTime = EarliestPossibleCreationTime;

			var safe = new WalletManager1(password, walletFilePath,  (DateTimeOffset)creationTime, mnemonic);
			safe.Save(password, walletFilePath,  safe.CreationTime);
			return safe;
		}

		private Mnemonic SetSeed(string password, Mnemonic mnemonic = null)
		{
			mnemonic = mnemonic ?? new Mnemonic(Wordlist.English, WordCount.Twelve);

			ExtKey = mnemonic.DeriveExtKey(password);

			return mnemonic;
		}

		private void SetSeed(ExtKey seedExtKey) => ExtKey = seedExtKey;

		private void Save(string password, string walletFilePath,  DateTimeOffset creationTime)
		{
			if (File.Exists(walletFilePath))
				throw new NotSupportedException($"Wallet already exists at {walletFilePath}");

			var directoryPath = Path.GetDirectoryName(Path.GetFullPath(walletFilePath));
			if (directoryPath != null) Directory.CreateDirectory(directoryPath);

			var privateKey = ExtKey.PrivateKey;
			var chainCode = ExtKey.ChainCode;

			var encryptedBitcoinPrivateKeyString = privateKey.GetEncryptedBitcoinSecret(password, network).ToWif();
			var chainCodeString = Convert.ToBase64String(chainCode);

			var networkString = network.ToString();

			var creationTimeString = creationTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

			WalletFileSerializer.Serialize(
				walletFilePath,
				encryptedBitcoinPrivateKeyString,
				chainCodeString,
				networkString,
				creationTimeString);
		}

		public static WalletManager1 Load(string password, string walletFilePath)
		{
			if (!File.Exists(walletFilePath))
				throw new ArgumentException($"No wallet file found at {walletFilePath}");

			var walletFileRawContent = WalletFileSerializer.Deserialize(walletFilePath);

			var encryptedBitcoinPrivateKeyString = walletFileRawContent.EncryptedSeed;
			var chainCodeString = walletFileRawContent.ChainCode;

			var chainCode = Convert.FromBase64String(chainCodeString);

			 
			var networkString = walletFileRawContent.Network;
		 

			DateTimeOffset creationTime = DateTimeOffset.ParseExact(walletFileRawContent.CreationTime, "yyyy-MM-dd", CultureInfo.InvariantCulture);

			var safe = new WalletManager1(password, walletFilePath,  creationTime);

			var privateKey = Key.Parse(encryptedBitcoinPrivateKeyString, password, safe.network);
			var seedExtKey = new ExtKey(privateKey, chainCode);
			safe.SetSeed(seedExtKey);

			return safe;
		}

		public BitcoinExtKey FindPrivateKey(BitcoinAddress address, int stopSearchAfterIteration = 100000, SafeAccount account = null)
		{
			for (int i = 0; i < stopSearchAfterIteration; i++)
			{
				if (GetAddress(i, HdPathType.Receive, account) == address)
					return GetBitcoinExtKey(i, HdPathType.Receive, account);
				if (GetAddress(i, HdPathType.Change, account) == address)
					return GetBitcoinExtKey(i, HdPathType.Change, account);
				if (GetAddress(i, HdPathType.NonHardened, account) == address)
					return GetBitcoinExtKey(i, HdPathType.NonHardened, account);
			}

			throw new KeyNotFoundException(address.ToString());
		}

		public BitcoinExtKey GetBitcoinExtKey(int? index = null, HdPathType hdPathType = HdPathType.Receive, SafeAccount account = null)
		{

			// return $"m/44'/{coinType}'/{accountIndex}'";

			string firstPart = "";
			if (account != null)
			{
				firstPart += Hierarchy.GetPathString(account) + "/";
			}

			firstPart += Hierarchy.GetPathString(hdPathType);
			string lastPart;
			if (index == null)
			{
				lastPart = "";
			}
			else if (hdPathType == HdPathType.NonHardened)
			{
				lastPart = $"/{index}";
			}
			else
			{
				lastPart = $"/{index}'";
			}
			

		


			KeyPath keyPath = new KeyPath(firstPart + lastPart);
 

			return ExtKey.Derive(keyPath).GetWif(network);
		}

		public string GetCreationTimeString()
		{
			return CreationTime.ToString("s", CultureInfo.InvariantCulture);
		}

		public void Delete()
		{
			if (File.Exists(WalletFilePath))
				File.Delete(WalletFilePath);
		}
	}
}
