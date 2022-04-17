using wallet.core;
using wallet.ui.Elements;
using wallet.ui.utility;
using wallet.ui.View;

namespace wallet.ui
{
    public partial class FrmMain : Form
    {

        public FrmMain()
        {
            InitializeComponent();
        }

        private async void bnn_CreateWallet_Click(object sender, EventArgs e)
        {
            FrmCreateWallet frm = new FrmCreateWallet();
            frm.ShowDialog();
            await _LoadAllWallets();

        }

        private void btn_RecoverWallet_Click(object sender, EventArgs e)
        {
            

            //FrmRecoverWallet frm = new FrmRecoverWallet();
            //frm.ShowDialog();
        }
        AddressManager addressManager = new AddressManager();
        private async void FrmMain_Load(object sender, EventArgs e)
        {
            await _LoadAllWallets();
        }

        private async Task _LoadAllWallets()
        {
            Panel_Wallets.Controls.Clear();

            foreach (var walletfind in new WalletManager().GetAllWalletInStore())
            {


                WalletItem _WalletItem1 = new WalletItem();
                _WalletItem1.Dock = DockStyle.Top;
                _WalletItem1._PreLoadWallet(walletfind);
                Panel_Wallets.Controls.Add(_WalletItem1);
            }


           
            processWalletMoney.UpdateCompleted += new EventHandler(_UpdateMoney);

        }

        private async void _UpdateMoney(object? sender, EventArgs e)
        {
            try
            {
                foreach(Control _con in Panel_Wallets.Controls)
                {
                    try
                    {
                        WalletItem walletItem = (WalletItem)_con;
                        walletItem.addressBalances = addressManager.addressBalances;
                       await walletItem._UpdateMoney();
                    }
                    catch { }
                }
            }
            catch { }

        }

        ProcessWalletMoney processWalletMoney = new ProcessWalletMoney();


        private void Btn_AvailableNetworks_Click(object sender, EventArgs e)
        {

        }

        private async void Timer_ReloadBalance_Tick(object sender, EventArgs e)
        {
            return;
            Timer_ReloadBalance.Enabled = false;
            Timer_ReloadBalance.Interval = (int)TimeSpan.FromMinutes(10).TotalMilliseconds;

            foreach (var walletfind in new WalletManager().GetAllWalletInStore())
            {
                await addressManager.GetWalletBalance(walletfind);
            }

            processWalletMoney.StartUpdate();

        }
    }
}