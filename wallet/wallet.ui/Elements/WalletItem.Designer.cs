namespace wallet.ui.Elements
{
    partial class WalletItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WalletItem));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Btn_getBalance = new System.Windows.Forms.Button();
            this.Btn_ShowAddress = new System.Windows.Forms.Button();
            this.Btn_Send = new System.Windows.Forms.Button();
            this.lbl_UnConfirmed = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_status = new System.Windows.Forms.Label();
            this.lbl_Confirmed = new System.Windows.Forms.Label();
            this.Lbl_NetworkName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_walletname = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Btn_getBalance);
            this.panel1.Controls.Add(this.Btn_ShowAddress);
            this.panel1.Controls.Add(this.Btn_Send);
            this.panel1.Controls.Add(this.lbl_UnConfirmed);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbl_status);
            this.panel1.Controls.Add(this.lbl_Confirmed);
            this.panel1.Controls.Add(this.Lbl_NetworkName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbl_walletname);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(609, 92);
            this.panel1.TabIndex = 0;
            // 
            // Btn_getBalance
            // 
            this.Btn_getBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_getBalance.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Btn_getBalance.Location = new System.Drawing.Point(350, 59);
            this.Btn_getBalance.Name = "Btn_getBalance";
            this.Btn_getBalance.Size = new System.Drawing.Size(84, 29);
            this.Btn_getBalance.TabIndex = 3;
            this.Btn_getBalance.Text = "Balance";
            this.Btn_getBalance.UseVisualStyleBackColor = true;
            this.Btn_getBalance.Click += new System.EventHandler(this.Btn_getBalance_ClickAsync);
            // 
            // Btn_ShowAddress
            // 
            this.Btn_ShowAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_ShowAddress.Enabled = false;
            this.Btn_ShowAddress.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Btn_ShowAddress.Location = new System.Drawing.Point(520, 59);
            this.Btn_ShowAddress.Name = "Btn_ShowAddress";
            this.Btn_ShowAddress.Size = new System.Drawing.Size(84, 29);
            this.Btn_ShowAddress.TabIndex = 3;
            this.Btn_ShowAddress.Text = "Address";
            this.Btn_ShowAddress.UseVisualStyleBackColor = true;
            this.Btn_ShowAddress.Click += new System.EventHandler(this.Btn_ShowAddress_Click);
            // 
            // Btn_Send
            // 
            this.Btn_Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Send.Enabled = false;
            this.Btn_Send.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Btn_Send.Location = new System.Drawing.Point(434, 59);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(84, 29);
            this.Btn_Send.TabIndex = 3;
            this.Btn_Send.Text = "Send";
            this.Btn_Send.UseVisualStyleBackColor = true;
            this.Btn_Send.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // lbl_UnConfirmed
            // 
            this.lbl_UnConfirmed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_UnConfirmed.AutoSize = true;
            this.lbl_UnConfirmed.ForeColor = System.Drawing.Color.Gray;
            this.lbl_UnConfirmed.Location = new System.Drawing.Point(451, 32);
            this.lbl_UnConfirmed.Name = "lbl_UnConfirmed";
            this.lbl_UnConfirmed.Size = new System.Drawing.Size(17, 20);
            this.lbl_UnConfirmed.TabIndex = 2;
            this.lbl_UnConfirmed.Text = "0";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(292, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "UnConfirmed Balance";
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(41, 61);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(15, 20);
            this.lbl_status.TabIndex = 2;
            this.lbl_status.Text = "-";
            // 
            // lbl_Confirmed
            // 
            this.lbl_Confirmed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Confirmed.AutoSize = true;
            this.lbl_Confirmed.Location = new System.Drawing.Point(451, 10);
            this.lbl_Confirmed.Name = "lbl_Confirmed";
            this.lbl_Confirmed.Size = new System.Drawing.Size(17, 20);
            this.lbl_Confirmed.TabIndex = 2;
            this.lbl_Confirmed.Text = "0";
            // 
            // Lbl_NetworkName
            // 
            this.Lbl_NetworkName.AutoSize = true;
            this.Lbl_NetworkName.ForeColor = System.Drawing.Color.Gray;
            this.Lbl_NetworkName.Location = new System.Drawing.Point(41, 30);
            this.Lbl_NetworkName.Name = "Lbl_NetworkName";
            this.Lbl_NetworkName.Size = new System.Drawing.Size(113, 20);
            this.Lbl_NetworkName.TabIndex = 2;
            this.Lbl_NetworkName.Text = "seniorblockcoin";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(292, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Confirmed Balance";
            // 
            // lbl_walletname
            // 
            this.lbl_walletname.AutoSize = true;
            this.lbl_walletname.Location = new System.Drawing.Point(41, 10);
            this.lbl_walletname.Name = "lbl_walletname";
            this.lbl_walletname.Size = new System.Drawing.Size(77, 20);
            this.lbl_walletname.TabIndex = 1;
            this.lbl_walletname.Text = "WalletSBC";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // WalletItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Name = "WalletItem";
            this.Size = new System.Drawing.Size(615, 102);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private Label lbl_walletname;
        private Label label1;
        private Label label2;
        private Label lbl_UnConfirmed;
        private Label lbl_Confirmed;
        private Label Lbl_NetworkName;
        private Button Btn_Send;
        private Button Btn_getBalance;
        private Label lbl_status;
        private Button Btn_ShowAddress;
    }
}
