using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallet.core
{
	public class WalletFileSerializer
	{
		


		public static Boolean Serialize(WalletFile _WalletFile)
		{
			try
			{
				String walletFilePath = WalletManager.WalletDirectory + _WalletFile.Name + ".wallet.json";

				var content = JsonConvert.SerializeObject(_WalletFile, Formatting.Indented);

				if (File.Exists(walletFilePath))
					return false;

				var directoryPath = Path.GetDirectoryName(Path.GetFullPath(walletFilePath));
				if (directoryPath != null) Directory.CreateDirectory(directoryPath);

				File.WriteAllText(walletFilePath, content);
				return true;
			}
			catch { }
			return false;
		}

		public static WalletFile Deserialize(string path)
        {
			try
			{

				if (!File.Exists(path))
					return new WalletFile();

				var contentString = File.ReadAllText(path);
				var walletFileSerializer = JsonConvert.DeserializeObject<WalletFile>(contentString);

				return walletFileSerializer;
			}
			catch { }
			return new WalletFile();
		}
	}
}
