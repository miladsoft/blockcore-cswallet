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
                MyWallet.UnspentOutputReferences = await _BlockTransection.GetSpendableTransactions(MyWallet);
                String _Hex = _BlockTransection.GetTransectionHex(Txt_Destination.Text, int.Parse(Txt_Amount.Text), Txt_Password.Text, MyWallet);
                if(_Hex != "")
                {
                    TxtLog.Text += Environment.NewLine + "Hex is ";
                    TxtLog.Text += _Hex;
                }
                else
                {
                    TxtLog.Text += "Failed To Create Hex";                    
                }

            }
            catch { }
        }
    }
}
