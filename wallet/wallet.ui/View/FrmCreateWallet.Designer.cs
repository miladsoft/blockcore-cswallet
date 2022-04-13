namespace wallet.ui
{
    partial class FrmCreateWallet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCreateWallet));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_SelectedCoin = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.txt_Passphrase = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Btn_SelectedNetwork = new System.Windows.Forms.Button();
            this.btn_GenerateMnemonic = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Mnemonic = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Create = new System.Windows.Forms.Button();
            this.errorProviderCW = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCW)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_SelectedCoin);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.txt_Password);
            this.groupBox1.Controls.Add(this.txt_Name);
            this.groupBox1.Controls.Add(this.txt_Passphrase);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Btn_SelectedNetwork);
            this.groupBox1.Controls.Add(this.btn_GenerateMnemonic);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_Mnemonic);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(585, 229);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lbl_SelectedCoin
            // 
            this.lbl_SelectedCoin.AutoSize = true;
            this.lbl_SelectedCoin.Location = new System.Drawing.Point(385, 24);
            this.lbl_SelectedCoin.Name = "lbl_SelectedCoin";
            this.lbl_SelectedCoin.Size = new System.Drawing.Size(71, 15);
            this.lbl_SelectedCoin.TabIndex = 10;
            this.lbl_SelectedCoin.Text = "not selected";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(351, 18);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // txt_Password
            // 
            this.txt_Password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Password.Location = new System.Drawing.Point(104, 189);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.Size = new System.Drawing.Size(350, 23);
            this.txt_Password.TabIndex = 8;
            // 
            // txt_Name
            // 
            this.txt_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Name.Location = new System.Drawing.Point(104, 160);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(350, 23);
            this.txt_Name.TabIndex = 7;
            // 
            // txt_Passphrase
            // 
            this.txt_Passphrase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Passphrase.Location = new System.Drawing.Point(104, 131);
            this.txt_Passphrase.Name = "txt_Passphrase";
            this.txt_Passphrase.Size = new System.Drawing.Size(350, 23);
            this.txt_Passphrase.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Passphrase";
            // 
            // Btn_SelectedNetwork
            // 
            this.Btn_SelectedNetwork.ForeColor = System.Drawing.Color.Navy;
            this.Btn_SelectedNetwork.Location = new System.Drawing.Point(104, 18);
            this.Btn_SelectedNetwork.Name = "Btn_SelectedNetwork";
            this.Btn_SelectedNetwork.Size = new System.Drawing.Size(138, 26);
            this.Btn_SelectedNetwork.TabIndex = 2;
            this.Btn_SelectedNetwork.Text = "Selected Network";
            this.Btn_SelectedNetwork.UseVisualStyleBackColor = true;
            this.Btn_SelectedNetwork.Click += new System.EventHandler(this.Btn_SelectedCoin_Click);
            // 
            // btn_GenerateMnemonic
            // 
            this.btn_GenerateMnemonic.ForeColor = System.Drawing.Color.Navy;
            this.btn_GenerateMnemonic.Location = new System.Drawing.Point(459, 54);
            this.btn_GenerateMnemonic.Name = "btn_GenerateMnemonic";
            this.btn_GenerateMnemonic.Size = new System.Drawing.Size(119, 70);
            this.btn_GenerateMnemonic.TabIndex = 2;
            this.btn_GenerateMnemonic.Text = "Generate Mnemonic";
            this.btn_GenerateMnemonic.UseVisualStyleBackColor = true;
            this.btn_GenerateMnemonic.Click += new System.EventHandler(this.btn_GenerateMnemonic_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mnemonic";
            // 
            // txt_Mnemonic
            // 
            this.txt_Mnemonic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Mnemonic.Location = new System.Drawing.Point(104, 54);
            this.txt_Mnemonic.Multiline = true;
            this.txt_Mnemonic.Name = "txt_Mnemonic";
            this.txt_Mnemonic.Size = new System.Drawing.Size(350, 70);
            this.txt_Mnemonic.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_Cancel);
            this.groupBox2.Controls.Add(this.btn_Create);
            this.groupBox2.Location = new System.Drawing.Point(10, 247);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(587, 53);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.ForeColor = System.Drawing.Color.Red;
            this.btn_Cancel.Location = new System.Drawing.Point(424, 21);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Create
            // 
            this.btn_Create.ForeColor = System.Drawing.Color.Green;
            this.btn_Create.Location = new System.Drawing.Point(505, 21);
            this.btn_Create.Name = "btn_Create";
            this.btn_Create.Size = new System.Drawing.Size(75, 23);
            this.btn_Create.TabIndex = 0;
            this.btn_Create.Text = "Create";
            this.btn_Create.UseVisualStyleBackColor = true;
            this.btn_Create.Click += new System.EventHandler(this.btn_Create_Click);
            // 
            // errorProviderCW
            // 
            this.errorProviderCW.ContainerControl = this;
            // 
            // FrmCreateWallet
            // 
            this.AcceptButton = this.btn_Create;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(606, 308);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCreateWallet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Wallet";
            this.Load += new System.EventHandler(this.FrmCreateWallet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderCW)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private Button btn_GenerateMnemonic;
        private Label label1;
        private TextBox txt_Mnemonic;
        private GroupBox groupBox2;
        private Button btn_Cancel;
        private Button btn_Create;
        private TextBox txt_Password;
        private TextBox txt_Name;
        private TextBox txt_Passphrase;
        private Label label4;
        private Label label3;
        private Label label2;
        private ErrorProvider errorProviderCW;
        private Button Btn_SelectedNetwork;
        private PictureBox pictureBox1;
        private Label lbl_SelectedCoin;
    }
}