using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallet.core
{
    public class AddressBalance
    {
        public string address { get; set; }
        public long balance { get; set; }
        public long totalReceived { get; set; }
        public long totalStake { get; set; }
        public int totalMine { get; set; }
        public long totalSent { get; set; }
        public int totalReceivedCount { get; set; }
        public int totalSentCount { get; set; }
        public int totalStakeCount { get; set; }
        public int totalMineCount { get; set; }
        public int pendingSent { get; set; }
        public int pendingReceived { get; set; }
    }
}
