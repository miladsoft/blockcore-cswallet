namespace wallet.ui.View
{
    partial class FrmAvailableNetworks
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_info = new System.Windows.Forms.Label();
            this.listView_networks = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(517, 22);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(517, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Available Networks";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Controls.Add(this.lbl_info);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 244);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(517, 22);
            this.panel2.TabIndex = 1;
            // 
            // lbl_info
            // 
            this.lbl_info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_info.Location = new System.Drawing.Point(0, 0);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(517, 22);
            this.lbl_info.TabIndex = 0;
            this.lbl_info.Text = "double click in item";
            this.lbl_info.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listView_networks
            // 
            this.listView_networks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_networks.Location = new System.Drawing.Point(0, 22);
            this.listView_networks.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listView_networks.Name = "listView_networks";
            this.listView_networks.Size = new System.Drawing.Size(517, 222);
            this.listView_networks.TabIndex = 2;
            this.listView_networks.UseCompatibleStateImageBehavior = false;
            this.listView_networks.DoubleClick += new System.EventHandler(this.listView_networks_DoubleClick);
            this.listView_networks.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView_networks_MouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FrmAvailableNetworks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(517, 266);
            this.Controls.Add(this.listView_networks);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmAvailableNetworks";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Available Networks";
            this.Load += new System.EventHandler(this.FrmAvailableNetworks_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Label lbl_info;
        private ListView listView_networks;
        private ImageList imageList1;
    }
}