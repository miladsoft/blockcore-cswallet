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
    public partial class FrmRecoverWallet : Form
    {
        public FrmRecoverWallet()
        {
            InitializeComponent();
        }

        private void btn_Create_Click(object sender, EventArgs e)
        {
            errorProviderCW.Clear();

            //------------------

            WalletManager _WalletManager = new WalletManager();

            NewWalletRequst _WalletObject = new NewWalletRequst();



            if (!string.IsNullOrEmpty(txt_Mnemonic.Text))
            {
                _WalletObject.mnemonic = txt_Mnemonic.Text.Trim();
            }
            else
            {
                errorProviderCW.SetError(txt_Mnemonic, "Please Enter Mnemonic");
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

          //  _WalletManager.RecoverWallet(_WalletObject);
 
        }
    }
}
