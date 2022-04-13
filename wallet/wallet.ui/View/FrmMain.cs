using wallet.core;

namespace wallet.ui
{
    public partial class FrmMain : Form
    {

        public FrmMain()
        {
            InitializeComponent();
        }

        private void bnn_CreateWallet_Click(object sender, EventArgs e)
        {
            FrmCreateWallet frm = new FrmCreateWallet();
            frm.ShowDialog();
            

        }

        private void btn_RecoverWallet_Click(object sender, EventArgs e)
        {
            FrmRecoverWallet frm = new FrmRecoverWallet();
            frm.ShowDialog();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }
    }
}