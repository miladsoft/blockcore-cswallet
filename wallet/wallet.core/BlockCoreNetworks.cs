using Blockcore.Networks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace wallet.core
{
    public class BlockCoreNetworks
    {
        public List<Network> GetAllNetworks()
        {
            var networks = new List<Network>();
            networks.Add(Blockcore.Networks.SeniorBlockCoin.Networks.Networks.SeniorBlockCoin.Mainnet());
            networks.Add(Blockcore.Networks.RoyalSportsCity.Networks.Networks.RoyalSportsCity.Mainnet());
            networks.Add(Blockcore.Networks.City.Networks.Networks.City.Mainnet());
            networks.Add(Blockcore.Networks.Cybits.Networks.Cybits.Mainnet());

            return networks;
        }

        public Network GetNetwork(String Networkname)
        {


            return GetAllNetworks().First(li => li.CoinTicker == Networkname);
        }



        public int CoinType(Network network)
        {
            if (network.CoinTicker == "SBC")
            { return 5006; }

            if (network.CoinTicker == "RSC")
            { return 6599; }


            if (network.CoinTicker == "CITY")
            { return 1926; }

            if (network.CoinTicker == "CY")
            { return 3601; }

            return 0;
        }

        public String IndexerUrl()
        {
            return @"indexer.blockcore.net";
        }

        public String GetIndexerUrl(Network network)
        {
            return @"https://" + network.CoinTicker + "." + IndexerUrl() + @"/";
        }


    }
}
