namespace wallet.core
{
    public class indexerAdress
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

    // Root myDeserializedClass = JsonConvert.DeserializeObject<AdressTrx>(myJsonResponse);
    public class AdressTrx
    {
        public string entryType { get; set; }
        public string transactionHash { get; set; }
        public int value { get; set; }
        public int blockIndex { get; set; }
        public int confirmations { get; set; }
    }


    // BlockInfo myDeserializedClass = JsonConvert.DeserializeObject<BlockInfo>(myJsonResponse);
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




}