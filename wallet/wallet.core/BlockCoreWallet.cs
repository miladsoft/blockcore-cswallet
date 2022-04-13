
using Blockcore.Features.Wallet.Types;
using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallet.core
{
    public class BlockCoreWallet
    {
        public String NetworkName { get; set; }
        public Mnemonic mnemonic { get; set; }
        public String WalletName { get; set; }
        public String Password { get; set; }

        public String Passphrase { get; set; }

        public Key PrivateKey { get; set; }

        public ExtPubKey accountExtPubKey { get; set; }

        public DateTimeOffset CreationTime { get; set; }

        public ExtKey SeedExtKey { get; set; }

        public Blockcore.Features.Wallet.Types.HdAccount HdAccount { get; set; }

        public List<Blockcore.Features.Wallet.Types.HdAddress> ReceivingAddresses { get; set; }

        public List<Blockcore.Features.Wallet.Types.HdAddress> ChangeAddresses { get; set; }

        public List<AdressData> AdressDatas { get; set; }
        public List<AddressValue> AddressValues { get; set; }


    }

    public class AdressData
    {
        public HdAddress HdAddress { get; set; }

        public List<AdressTrx> AdressTrxes { get; set; }
        public List<BlockInfo> blockInfo { get; set; }
    }

    public class AddressValue
    {
        public HdAddress Address { get; set; }
 
        public Double value { get; set; }

    }

}
