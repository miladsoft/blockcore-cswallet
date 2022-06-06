using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallet.core
{
    public class AddressManager
    {

        public List<AddressBalance> addressBalances { get; set; }   

        public AddressManager()
        {
            addressBalances = new List<AddressBalance>();
        }



        public async Task<List<AddressBalance>> GetWalletBalance(WalletFile _WalletFile)
        {
            addressBalances = new List<AddressBalance>();
            try
            {

                var _Adress = _WalletFile.hdAccount.GetCombinedAddresses();

                //myDeserializedClass.balance * 0.00000001;

                Double balanceConfirmedAmount = 0;
                Double balanceUnConfirmedAmount = 0;
                foreach (var _adr in _WalletFile.hdAccount.InternalAddresses)
                {
                   var  balance  = await GetAddressBalance(_adr.Bech32Address);
                    
                    if (balance != null)
                    {
                        addressBalances.Add(balance);
                        try
                        {
                            balanceConfirmedAmount += (balance.balance * 0.00000001);                            
                        }
                        catch { }
                    }
                }

                foreach (var _adr in _WalletFile.hdAccount.ExternalAddresses)
                {
                    var balance = await GetAddressBalance(_adr.Bech32Address);

                    if (balance != null)
                    {
                        addressBalances.Add(balance);
                        try
                        {
                            balanceConfirmedAmount += (balance.balance * 0.00000001);
                        }
                        catch { }
                    }
                }

                 
            }
            catch { }
            return addressBalances;
        }

        public async Task<AddressBalance> GetAddressBalance(String SgwitAddress)
        {
            try
            {

                if(SgwitAddress == "sbc1qqvghruuhasdg2nfgzslvaccunj9m8f2nsp5vmm")
                {

                }

                if (SgwitAddress == "sbc1qelhmpvv2krg3euapk88cfarn2kmjygxnt8jjy5")
                {

                }
                if (SgwitAddress == "sbc1qknsjhqeyt7m9dvxxrtfq4p300tf3p32tq8m8ky")
                {

                }

                if (SgwitAddress == "sbc1q3k54excyyq3a4t2tvz4q7hgset43wnwst6ypnn")
                {

                }

                if (SgwitAddress == "sbc1qdnmepfylnhrh6w9rux47qus824a8n9tch7egzz")
                {

                }

                if (SgwitAddress == "sbc1q6fjdc87sttrden9hk204dvj23g29lfj06vmxup")
                {

                }


                if (SgwitAddress == "sbc1q0n7v63javqagyr3gkuz2gqeevv3ver4hrq0s9c")
                {

                }
                if (SgwitAddress == "sbc1qzv5apv05f05k0yw8tv423455q6kl8uk38gg2ls")
                {

                }

                if (SgwitAddress == "sbc1qc5qxfgu29gn49cuxm5wdpvp8nm3fmewuf2lvft")
                {

                }




                //  http://localhost:15000/


                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://sbc.indexer.blockcore.net/api/query/address/" + SgwitAddress),
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    AddressBalance myDeserializedClass = JsonConvert.DeserializeObject<AddressBalance>(body.ToString());
                 
                    return myDeserializedClass;
                    //myDeserializedClass.balance * 0.00000001;
                }
            }
            catch { }
            return null;
        }

         
    }
}
