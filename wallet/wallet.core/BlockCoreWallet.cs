using Blockcore.Features.Wallet.Types;
using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallet.core
{
    public class NewWalletRequst
    {
        public String NetworkName { get; set; }
        public String mnemonic { get; set; }
        public String WalletName { get; set; }
        public String Password { get; set; }
        public String Passphrase { get; set; }         
        public DateTimeOffset CreationTime { get; set; } 

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

    public class BlockInfo
    {
        public string blockHash { get; set; }
        public int blockIndex { get; set; }
        public int blockSize { get; set; }
        public int blockTime { get; set; }
        public string previousBlockHash { get; set; }
        public bool syncComplete { get; set; }
        public int transactionCount { get; set; }
        public int confirmations { get; set; }
        public string bits { get; set; }
        public double difficulty { get; set; }
        public string chainWork { get; set; }
        public string merkleroot { get; set; }
        public int nonce { get; set; }
        public int version { get; set; }
        public string posBlockSignature { get; set; }
        public string posModifierv2 { get; set; }
        public string posFlags { get; set; }
        public string posHashProof { get; set; }
        public string posBlockTrust { get; set; }
        public string posChainTrust { get; set; }
    }
 


    public class Outpoint
    {
        public string transactionId { get; set; }
        public int outputIndex { get; set; }
    }

    public class AdressTrx
    {
        public Outpoint outpoint { get; set; }
        public string address { get; set; }
        public string scriptHex { get; set; }
        public Double value { get; set; }
        public int blockIndex { get; set; }
        public bool coinBase { get; set; }
        public bool coinStake { get; set; }
    }

}
