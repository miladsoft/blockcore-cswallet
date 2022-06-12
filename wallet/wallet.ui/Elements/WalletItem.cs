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

                foreach (var _adr in addressBalances)
                {
                    try
                    {
                        balanceConfirmedAmount += (_adr.balance * 0.00000001);
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
                _FrmWallet.addressBalances = addressBalances;
                _FrmWallet.Show();
            }
            catch { }
        }

        private async void Btn_getBalance_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                Btn_getBalance.Enabled = false;
                lbl_status.Text = "calculate balance . wait";
                DateTime _Strat = DateTime.Now;

                addressBalances = new List<AddressBalance>();

                foreach (var walletfind in new WalletManager().GetAllWalletInStore())
                {
                    addressBalances = await new AddressManager().GetWalletBalance(MyWallet);
                }

                await _UpdateMoney();

                DateTime _End = DateTime.Now;
                lbl_status.Text = "-";
                Btn_getBalance.Enabled = true;

            }
            catch { }
        }

        private void Btn_Addresses_Click(object sender, EventArgs e)
        {
            try
            {
                FrmAddress _FrmAddress = new FrmAddress();
                _FrmAddress.MyWallet = MyWallet;
                _FrmAddress.addressBalances = addressBalances;
                _FrmAddress.Show();
            }
            catch { }

        }
    }
}
