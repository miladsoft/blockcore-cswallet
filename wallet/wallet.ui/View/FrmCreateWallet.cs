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

namespace wallet.ui
{
    public partial class FrmCreateWallet : Form
    {
        public FrmCreateWallet()
        {
            InitializeComponent();
        }

        private void btn_GenerateMnemonic_Click(object sender, EventArgs e)
        {
            txt_Mnemonic.Text = WalletManager.GenerateMnemonic(wordCount: 24).ToString();
        }

        private void btn_Create_Click(object sender, EventArgs e)
        {
            errorProviderCW.Clear();

            //------------------

            WalletManager _WalletManager = new WalletManager();

            BlockCoreWallet _WalletObject = new BlockCoreWallet();

            if (!string.IsNullOrEmpty(txt_Mnemonic.Text))
            {
                _WalletObject.mnemonic = new NBitcoin.Mnemonic(txt_Mnemonic.Text.Trim());
            }
            else
            {
                errorProviderCW.SetError(txt_Mnemonic, "Please generate Mnemonic");
                return;
            }
            //--------------------

            if (!string.IsNullOrEmpty(txt_Passphrase.Text))
            {
                _WalletObject.Passphrase = txt_Passphrase.Text.Trim();
            }
            else
            {
                errorProviderCW.SetError(txt_Passphrase, "Please Enter Passphrase");
                return;
            }

            //----------------------

            if (!string.IsNullOrEmpty(txt_Name.Text))
            {
                _WalletObject.WalletName = txt_Name.Text.Trim();
            }
            else
            {
                errorProviderCW.SetError(txt_Name, "Please Enter Name");
                return;
            }
            //----------------------
           
            if (!string.IsNullOrEmpty(txt_Password.Text))
            {
                _WalletObject.Password = txt_Password.Text.Trim();
            }
            else
            {
                errorProviderCW.SetError(txt_Password, "Please Enter Password");
                return;
            }
            //----------------------

            _WalletObject.NetworkName = "SeniorBlockCoinMain";

            Boolean _Success = _WalletManager.CreateNewWallet(_WalletObject);
            if (_Success == false)
            {
                MessageBox.Show("Wallet Not Created");
                return;
            }
        }
    }
}
