using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallet.core
{
    public class WalletObject
    {

        public Mnemonic mnemonic { get; set; }

        public String WalletName { get; set; }

        public String Passphrase { get; set; }

        public String Password { get; set; }
        public String Account { get; set; }
        


    }
}
