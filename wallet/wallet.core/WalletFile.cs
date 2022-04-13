using Blockcore.Features.Wallet.Types;
using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace wallet.core
{
 
	public class WalletFile
    {		 
		public string Name { get; set; }
		public string EncryptedSeed { get; set; }
		public string ChainCode { get; set; }
		public string Network { get; set; }
		public string CreationTime { get; set; }
		public int coinType { get; set; }
		public HdAccount hdAccount { get; set; }

		public Double ConfirmedAmount { get; set; }
		public Double UnConfirmedAmount { get; set; }
		public List<UnspentOutputReference> UnspentOutputReferences { get; set; }


	}
}
