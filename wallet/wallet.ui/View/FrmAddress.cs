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

namespace wallet.ui.View
{
    public partial class FrmAddress : Form
    {

        public FrmAddress()
        {
            InitializeComponent();
        }

        public WalletFile MyWallet { get; set; }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }


        public async Task _PreLoadWallet()
        {
            try
            {
                
                Network network = new WalletManager().GetNetwork(MyWallet.Network);
                pictureBox1.Image = Utilities.LoadBase64(network.FavoriteIcon());

                lbl_walletname.Text = MyWallet.Name;
                Lbl_NetworkName.Text = network.CoinTicker + " [ " + network.Name + " ] ";
                lbl_Confirmed.Text = "0";
                lbl_UnConfirmed.Text = "0";

            }
            catch { }
        }
        public List<AddressBalance> addressBalances { get; set; }

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

        private async void FrmAddress_Load(object sender, EventArgs e)
        {
            await _ShowAddress();

            await _PreLoadWallet();

            await _UpdateMoney();

        
        }

        private async Task _ShowAddress()
        {
            Txt_ExternalAddresses.Text = "";
            Txt_InternalAddresses.Text = "";
            Txt_AddressBalance.Text = "";
            try
            {
                foreach (var _Adr in MyWallet.hdAccount.ExternalAddresses)
                {
                    Txt_ExternalAddresses.AppendText(_Adr.Bech32Address + Environment.NewLine);
                }

                }
            catch { }


            try
            {
                foreach (var _Adr in MyWallet.hdAccount.InternalAddresses)
                {
                    Txt_InternalAddresses.AppendText(_Adr.Bech32Address + Environment.NewLine);
                }

            }
            catch { }


            try
            {
                foreach (var _Adr in addressBalances)
                {

                    var _Balance = (_Adr.balance * 0.00000001);

                    Txt_AddressBalance.AppendText(_Adr.address + " balance = " + _Balance.ToString() + Environment.NewLine);
                }

            }
            catch { }

        }
    }
}
