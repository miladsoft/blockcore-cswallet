namespace wallet.ui
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.bnn_CreateWallet = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage0 = new System.Windows.Forms.TabPage();
            this.Panel_Wallets = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.Btn_AvailableNetworks = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Timer_ReloadBalance = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage0.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bnn_CreateWallet
            // 
            this.bnn_CreateWallet.Location = new System.Drawing.Point(17, 29);
            this.bnn_CreateWallet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bnn_CreateWallet.Name = "bnn_CreateWallet";
            this.bnn_CreateWallet.Size = new System.Drawing.Size(129, 31);
            this.bnn_CreateWallet.TabIndex = 0;
            this.bnn_CreateWallet.Text = "Create Wallet";
            this.bnn_CreateWallet.UseVisualStyleBackColor = true;
            this.bnn_CreateWallet.Click += new System.EventHandler(this.bnn_CreateWallet_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.bnn_CreateWallet);
            this.groupBox1.Location = new System.Drawing.Point(14, 36);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(889, 84);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage0);
            this.tabControl1.Location = new System.Drawing.Point(14, 128);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(893, 290);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage0
            // 
            this.tabPage0.Controls.Add(this.Panel_Wallets);
            this.tabPage0.Location = new System.Drawing.Point(4, 29);
            this.tabPage0.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage0.Name = "tabPage0";
            this.tabPage0.Size = new System.Drawing.Size(885, 257);
            this.tabPage0.TabIndex = 4;
            this.tabPage0.Text = "Wallets";
            this.tabPage0.UseVisualStyleBackColor = true;
            // 
            // Panel_Wallets
            // 
            this.Panel_Wallets.AutoScroll = true;
            this.Panel_Wallets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Wallets.Location = new System.Drawing.Point(0, 0);
            this.Panel_Wallets.Name = "Panel_Wallets";
            this.Panel_Wallets.Size = new System.Drawing.Size(885, 257);
            this.Panel_Wallets.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 453);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(920, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(920, 30);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.Btn_AvailableNetworks});
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.settingToolStripMenuItem.Text = "Setting";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(217, 6);
            // 
            // Btn_AvailableNetworks
            // 
            this.Btn_AvailableNetworks.Name = "Btn_AvailableNetworks";
            this.Btn_AvailableNetworks.Size = new System.Drawing.Size(220, 26);
            this.Btn_AvailableNetworks.Text = "Available Networks";
            this.Btn_AvailableNetworks.Click += new System.EventHandler(this.Btn_AvailableNetworks_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // Timer_ReloadBalance
            // 
            this.Timer_ReloadBalance.Enabled = true;
            this.Timer_ReloadBalance.Tick += new System.EventHandler(this.Timer_ReloadBalance_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 475);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(797, 518);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blockcore Wallet";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage0.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button bnn_CreateWallet;
        private GroupBox groupBox1;
        private TabControl tabControl1;
        private StatusStrip statusStrip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private TabPage tabPage0;
        private ToolStripMenuItem settingToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem Btn_AvailableNetworks;
        private Panel Panel_Wallets;
        private System.Windows.Forms.Timer Timer_ReloadBalance;
    }
}