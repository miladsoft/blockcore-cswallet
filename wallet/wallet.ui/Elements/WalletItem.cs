using Blockcore.Networks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wallet.core;
using wallet.ui.utility;
using wallet.ui.View;

namespace wallet.ui.Elements
{
    public partial class WalletItem : UserControl
    {
        public WalletItem()
        {
            InitializeComponent();
        }

        private WalletFile MyWallet;
        public List<AddressBalance> addressBalances { get; set; }

        public async Task _PreLoadWallet(WalletFile walletFile)
        {
            try
            {
                MyWallet = walletFile;
                Network network = new WalletManager().GetNetwork(MyWallet.Network);
                pictureBox1.Image = Utilities.LoadBase64(network.FavoriteIcon());

                lbl_walletname.Text = MyWallet.Name;
                Lbl_NetworkName.Text = network.CoinTicker + " [ " + network.Name + " ] ";
                lbl_Confirmed.Text = "0";
                lbl_UnConfirmed.Text = "0";

            }
            catch { }
        }

        public async Task _UpdateMoney()
        {
            Double balanceConfirmedAmount = 0;
            Double balanceUnConfirmedAmount = 0;

            try
            {

                foreach (var _adr in MyWallet.hdAccount.InternalAddresses)
                { 
                    try
                    {
                        var balance = addressBalances.First(li => li.address == _adr.Bech32Address);
                        balanceConfirmedAmount += (balance.balance * 0.00000001);
                    }
                    catch { }
                }


                foreach (var _adr in MyWallet.hdAccount.ExternalAddresses)
                {
                    try
                    {
                        var balance = addressBalances.First(li => li.address == _adr.Bech32Address);
                        balanceConfirmedAmount += (balance.balance * 0.00000001);
                    }
                    catch { }
                }

            }
            catch { }

            MyWallet.ConfirmedAmount = balanceConfirmedAmount;
            MyWallet.UnConfirmedAmount = balanceUnConfirmedAmount;


            lbl_Confirmed.Text = balanceConfirmedAmount.ToString();

        }

        private void Btn_Send_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSend _FrmWallet = new FrmSend();
                _FrmWallet.MyWallet = MyWallet;
                _FrmWallet.Show();
            }
            catch { }

        }

        private void Lbl_NetworkName_Click(object sender, EventArgs e)
        {

        }

        private void lbl_walletname_Click(object sender, EventArgs e)
        {

        }
    }
}
