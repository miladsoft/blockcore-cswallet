namespace wallet.ui.View
{
    partial class FrmAddress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddress));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_status = new System.Windows.Forms.Label();
            this.Lbl_NetworkName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_walletname = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.Txt_InternalAddresses = new System.Windows.Forms.TextBox();
            this.Txt_ExternalAddresses = new System.Windows.Forms.TextBox();
            this.lbl_UnConfirmed = new System.Windows.Forms.Label();
            this.lbl_Confirmed = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.Txt_AddressBalance = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_UnConfirmed);
            this.groupBox1.Controls.Add(this.lbl_Confirmed);
            this.groupBox1.Controls.Add(this.Btn_Close);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lbl_status);
            this.groupBox1.Controls.Add(this.Lbl_NetworkName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lbl_walletname);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 117);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Wallet Info";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(304, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "UnConfirmed Balance";
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(53, 84);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(15, 20);
            this.lbl_status.TabIndex = 6;
            this.lbl_status.Text = "-";
            // 
            // Lbl_NetworkName
            // 
            this.Lbl_NetworkName.AutoSize = true;
            this.Lbl_NetworkName.ForeColor = System.Drawing.Color.Gray;
            this.Lbl_NetworkName.Location = new System.Drawing.Point(53, 53);
            this.Lbl_NetworkName.Name = "Lbl_NetworkName";
            this.Lbl_NetworkName.Size = new System.Drawing.Size(113, 20);
            this.Lbl_NetworkName.TabIndex = 7;
            this.Lbl_NetworkName.Text = "seniorblockcoin";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(304, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Confirmed Balance";
            // 
            // lbl_walletname
            // 
            this.lbl_walletname.AutoSize = true;
            this.lbl_walletname.Location = new System.Drawing.Point(53, 33);
            this.lbl_walletname.Name = "lbl_walletname";
            this.lbl_walletname.Size = new System.Drawing.Size(77, 20);
            this.lbl_walletname.TabIndex = 4;
            this.lbl_walletname.Text = "WalletSBC";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(15, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 135);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(740, 328);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Txt_InternalAddresses);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(732, 295);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "InternalAddresses";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Txt_ExternalAddresses);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(732, 295);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ExternalAddresses";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Btn_Close
            // 
            this.Btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Btn_Close.Location = new System.Drawing.Point(640, 75);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(84, 29);
            this.Btn_Close.TabIndex = 9;
            this.Btn_Close.Text = "Close";
            this.Btn_Close.UseVisualStyleBackColor = true;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Txt_InternalAddresses
            // 
            this.Txt_InternalAddresses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_InternalAddresses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Txt_InternalAddresses.Location = new System.Drawing.Point(3, 3);
            this.Txt_InternalAddresses.Multiline = true;
            this.Txt_InternalAddresses.Name = "Txt_InternalAddresses";
            this.Txt_InternalAddresses.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Txt_InternalAddresses.Size = new System.Drawing.Size(726, 289);
            this.Txt_InternalAddresses.TabIndex = 0;
            // 
            // Txt_ExternalAddresses
            // 
            this.Txt_ExternalAddresses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_ExternalAddresses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Txt_ExternalAddresses.Location = new System.Drawing.Point(3, 3);
            this.Txt_ExternalAddresses.Multiline = true;
            this.Txt_ExternalAddresses.Name = "Txt_ExternalAddresses";
            this.Txt_ExternalAddresses.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Txt_ExternalAddresses.Size = new System.Drawing.Size(726, 289);
            this.Txt_ExternalAddresses.TabIndex = 1;
            // 
            // lbl_UnConfirmed
            // 
            this.lbl_UnConfirmed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_UnConfirmed.AutoSize = true;
            this.lbl_UnConfirmed.ForeColor = System.Drawing.Color.Gray;
            this.lbl_UnConfirmed.Location = new System.Drawing.Point(492, 55);
            this.lbl_UnConfirmed.Name = "lbl_UnConfirmed";
            this.lbl_UnConfirmed.Size = new System.Drawing.Size(17, 20);
            this.lbl_UnConfirmed.TabIndex = 10;
            this.lbl_UnConfirmed.Text = "0";
            // 
            // lbl_Confirmed
            // 
            this.lbl_Confirmed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Confirmed.AutoSize = true;
            this.lbl_Confirmed.Location = new System.Drawing.Point(492, 33);
            this.lbl_Confirmed.Name = "lbl_Confirmed";
            this.lbl_Confirmed.Size = new System.Drawing.Size(17, 20);
            this.lbl_Confirmed.TabIndex = 11;
            this.lbl_Confirmed.Text = "0";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.Txt_AddressBalance);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(732, 295);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Balance";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Txt_AddressBalance
            // 
            this.Txt_AddressBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_AddressBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Txt_AddressBalance.Location = new System.Drawing.Point(3, 3);
            this.Txt_AddressBalance.Multiline = true;
            this.Txt_AddressBalance.Name = "Txt_AddressBalance";
            this.Txt_AddressBalance.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Txt_AddressBalance.Size = new System.Drawing.Size(726, 289);
            this.Txt_AddressBalance.TabIndex = 2;
            // 
            // FrmAddress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 475);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Address";
            this.Load += new System.EventHandler(this.FrmAddress_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private Label label2;
        private Label lbl_status;
        private Label Lbl_NetworkName;
        private Label label1;
        private Label lbl_walletname;
        private PictureBox pictureBox1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button Btn_Close;
        private TextBox Txt_InternalAddresses;
        private TextBox Txt_ExternalAddresses;
        private Label lbl_UnConfirmed;
        private Label lbl_Confirmed;
        private TabPage tabPage3;
        private TextBox Txt_AddressBalance;
    }
}