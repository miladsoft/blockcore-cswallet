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
