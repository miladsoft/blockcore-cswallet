﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallet.core
{
	public class WalletFileSerializer
	{
		[JsonConstructor]
		private WalletFileSerializer(string encryptedBitcoinPrivateKey, string chainCode, string network, string creationTime)
		{
			EncryptedSeed = encryptedBitcoinPrivateKey;
			ChainCode = chainCode;
			Network = network;
			CreationTime = creationTime;
		}

		// KEEP THEM PUBLIC OTHERWISE IT WILL NOT SERIALIZE!
		public string EncryptedSeed { get; set; }
		public string ChainCode { get; set; }
		public string Network { get; set; }
		public string CreationTime { get; set; }

		public static void Serialize(string walletFilePath, string encryptedBitcoinPrivateKey, string chainCode,
			string network, string creationTime)
		{
			var content =
				JsonConvert.SerializeObject(new WalletFileSerializer(encryptedBitcoinPrivateKey, chainCode, network, creationTime), Formatting.Indented);

			if (File.Exists(walletFilePath))
				throw new NotSupportedException($"Wallet file already exists at {walletFilePath}");

			var directoryPath = Path.GetDirectoryName(Path.GetFullPath(walletFilePath));
			if (directoryPath != null) Directory.CreateDirectory(directoryPath);

			File.WriteAllText(walletFilePath, content);
		}

		public static WalletFileSerializer Deserialize(string path)
		{
			if (!File.Exists(path))
				throw new FileNotFoundException($"Wallet not found at {path}");

			var contentString = File.ReadAllText(path);
			var walletFileSerializer = JsonConvert.DeserializeObject<WalletFileSerializer>(contentString);

			return new WalletFileSerializer(walletFileSerializer.EncryptedSeed, walletFileSerializer.ChainCode,
				walletFileSerializer.Network, walletFileSerializer.CreationTime);
		}
	}
}