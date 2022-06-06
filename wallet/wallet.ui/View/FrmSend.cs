using NBitcoin.Policy;
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

namespace wallet.ui.View
{
    public partial class FrmSend : Form
    {
        public FrmSend()
        {
            InitializeComponent();
        }

        public WalletFile MyWallet { get; set; }
        private void FrmWallet_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text += MyWallet.Name;
            }
            catch { }
        }
        public List<AddressBalance> addressBalances { get; set; }

        BlockTransection _BlockTransection = new BlockTransection();

        private async void Btn_CreateTransaction_Click(object sender, EventArgs e)
        {

            // Mnemonic("travel west flush element churn hunt certain need frame noodle aisle slender jewel museum universe amused happy ability lyrics finger behave roast surge zebra");
            //Passphrase = "P@@sword!!200";
            //"P@@sword!!200";
            //"WalletSBC";
            //"SBC";

            try
            {
                var _ChangedAddress = MyWallet.hdAccount.ExternalAddresses.FirstOrDefault();
                var _Unspent = await _BlockTransection.GetSpendableTransactions(MyWallet);

                MyWallet.UnspentOutputReferences = _Unspent.ToList();

             

                TransactionPolicyError[] errors = null;
                String _Hex = "";

              // _Hex = new SendCoin().SendCoins(Txt_Password.Text, Txt_Amount.Text, _ChangedAddress, Txt_Destination.Text, MyWallet, out errors);

                //    _Hex = new SendCoin().SendCoins1(Txt_Password.Text, _ChangedAddress, Txt_Destination.Text, MyWallet);

           // _Hex = _BlockTransection.GetTransectionHex(Txt_Destination.Text, _ChangedAddress,  int.Parse(Txt_Amount.Text), Txt_Password.Text, MyWallet, out errors);
 _Hex = new  SendCoin().TXSendCoins(Txt_Password.Text, Txt_Amount.Text, _ChangedAddress, Txt_Destination.Text, MyWallet );


                if (_Hex != "")
                {
                    Txt_TransactionHex.Text =  _Hex;
                    TxtLog.Text += Environment.NewLine + "Hex is ";
                    TxtLog.Text += _Hex;
                }
                else
                {
                    TxtLog.Text += "Failed To Create Hex" + Environment.NewLine;
                    TxtLog.Text += "------ Error ---------- " + Environment.NewLine;
                    foreach (var error in errors)
                    {
                        TxtLog.Text += error + Environment.NewLine;
                    }
                }

            }
            catch { }
        }
    }
}
