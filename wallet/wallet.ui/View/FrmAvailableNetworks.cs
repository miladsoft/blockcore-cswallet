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
    public partial class FrmAvailableNetworks : Form
    {
        public FrmAvailableNetworks()
        {
            InitializeComponent();
        }

        private async void FrmAvailableNetworks_Load(object sender, EventArgs e)
        {
            await arangcomponent();

            await showAllNetworks();
        }

        private async Task showAllNetworks()
        {
            foreach (var network in new BlockCoreNetworks().GetAllNetworks())
            {
                var listViewItem = new ListViewItem();
                listViewItem.Text = network.CoinTicker;
                imageList1.Images.Add(network.CoinTicker, Utilities.LoadBase64(network.FavoriteIcon()));
                listViewItem.ToolTipText = network.Name;
                listViewItem.ImageKey = network.CoinTicker;

                listView_networks.Items.Add(listViewItem);
            }


        }

        private async Task arangcomponent()
        {

            SelectedDialog = DialogResult.No;

            ColumnHeader columnHeader2 = new ColumnHeader();
            ColumnHeader ColumnHeader3 = new ColumnHeader();
            ColumnHeader ColumnHeader4 = new ColumnHeader();
            ColumnHeader ColumnHeader5 = new ColumnHeader();
            ColumnHeader ColumnHeader6 = new ColumnHeader();
            ColumnHeader ColumnHeader1 = new ColumnHeader();
            ColumnHeader CHName = new ColumnHeader();
            listView_networks.Columns.AddRange(new ColumnHeader[] { CHName, columnHeader2, ColumnHeader3, ColumnHeader4, ColumnHeader5, ColumnHeader6, ColumnHeader1 });
            listView_networks.Dock = DockStyle.Fill;
            listView_networks.Font = new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Pixel, 0x86);
            //  listView_networks.HideSelection = false;
            listView_networks.LargeImageList = imageList1;






            listView_networks.SmallImageList = imageList1;
            listView_networks.Sorting = SortOrder.Ascending;


        }

        public Network SelectedNetwork { get; set; }
        public DialogResult SelectedDialog { get; set; }

        private void listView_networks_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ListViewItem item;
                try
                {
                    SelectedNetwork = new BlockCoreNetworks().GetAllNetworks().First(li => li.CoinTicker == this.listView_networks.SelectedItems[0].Text);
                    SelectedDialog = DialogResult.OK;
                    Close();
                }
                catch
                {
                    return;
                }
            }
            catch { }
        }

        private void listView_networks_MouseClick(object sender, MouseEventArgs e)
        {

            try
            {
                SelectedNetwork = new BlockCoreNetworks().GetAllNetworks().First(li => li.CoinTicker == this.listView_networks.SelectedItems[0].Text);
                lbl_info.Text = SelectedNetwork.CoinTicker + " [ " + SelectedNetwork.Name + " ]";
            }
            catch
            {
                return;
            }

        }
    }
}
