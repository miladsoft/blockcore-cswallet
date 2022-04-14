namespace wallet.ui.View
{
    partial class FrmSend
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSend));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Btn_CreateTransaction = new System.Windows.Forms.Button();
            this.Txt_Amount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Txt_Password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_Destination = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Btn_SendTransaction = new System.Windows.Forms.Button();
            this.Txt_TransactionHex = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TxtLog = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Btn_CreateTransaction);
            this.groupBox1.Controls.Add(this.Txt_Amount);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Txt_Password);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Txt_Destination);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(10, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(551, 132);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Send Coin";
            // 
            // Btn_CreateTransaction
            // 
            this.Btn_CreateTransaction.Location = new System.Drawing.Point(452, 98);
            this.Btn_CreateTransaction.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_CreateTransaction.Name = "Btn_CreateTransaction";
            this.Btn_CreateTransaction.Size = new System.Drawing.Size(82, 22);
            this.Btn_CreateTransaction.TabIndex = 2;
            this.Btn_CreateTransaction.Text = "Create Transaction";
            this.Btn_CreateTransaction.UseVisualStyleBackColor = true;
            this.Btn_CreateTransaction.Click += new System.EventHandler(this.Btn_CreateTransaction_Click);
            // 
            // Txt_Amount
            // 
            this.Txt_Amount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_Amount.Location = new System.Drawing.Point(171, 73);
            this.Txt_Amount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Txt_Amount.Name = "Txt_Amount";
            this.Txt_Amount.Size = new System.Drawing.Size(363, 23);
            this.Txt_Amount.TabIndex = 1;
            this.Txt_Amount.Text = "5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(97, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Amount";
            // 
            // Txt_Password
            // 
            this.Txt_Password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_Password.Location = new System.Drawing.Point(171, 48);
            this.Txt_Password.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Txt_Password.Name = "Txt_Password";
            this.Txt_Password.Size = new System.Drawing.Size(363, 23);
            this.Txt_Password.TabIndex = 1;
            this.Txt_Password.Text = "P@@sword!!200";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Wallet Password";
            // 
            // Txt_Destination
            // 
            this.Txt_Destination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_Destination.Location = new System.Drawing.Point(171, 23);
            this.Txt_Destination.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Txt_Destination.Name = "Txt_Destination";
            this.Txt_Destination.Size = new System.Drawing.Size(363, 23);
            this.Txt_Destination.TabIndex = 1;
            this.Txt_Destination.Text = "sbc1qj04a665g70tfcmrejrvmfrugrnyc3c8rfugdlr";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Destination Address";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Btn_SendTransaction);
            this.groupBox2.Controls.Add(this.Txt_TransactionHex);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(10, 152);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(551, 132);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Send Coin";
            // 
            // Btn_SendTransaction
            // 
            this.Btn_SendTransaction.Location = new System.Drawing.Point(452, 100);
            this.Btn_SendTransaction.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_SendTransaction.Name = "Btn_SendTransaction";
            this.Btn_SendTransaction.Size = new System.Drawing.Size(82, 22);
            this.Btn_SendTransaction.TabIndex = 2;
            this.Btn_SendTransaction.Text = "Send Transaction";
            this.Btn_SendTransaction.UseVisualStyleBackColor = true;
            // 
            // Txt_TransactionHex
            // 
            this.Txt_TransactionHex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_TransactionHex.Location = new System.Drawing.Point(27, 43);
            this.Txt_TransactionHex.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Txt_TransactionHex.Multiline = true;
            this.Txt_TransactionHex.Name = "Txt_TransactionHex";
            this.Txt_TransactionHex.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Txt_TransactionHex.Size = new System.Drawing.Size(507, 51);
            this.Txt_TransactionHex.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Transaction Hex";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TxtLog);
            this.groupBox3.Location = new System.Drawing.Point(10, 288);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(551, 132);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log";
            // 
            // TxtLog
            // 
            this.TxtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtLog.Location = new System.Drawing.Point(3, 18);
            this.TxtLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtLog.Multiline = true;
            this.TxtLog.Name = "TxtLog";
            this.TxtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtLog.Size = new System.Drawing.Size(545, 112);
            this.TxtLog.TabIndex = 1;
            // 
            // FrmSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 435);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmSend";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wallet = ";
            this.Load += new System.EventHandler(this.FrmWallet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private Button Btn_CreateTransaction;
        private TextBox Txt_Destination;
        private Label label1;
        private TextBox Txt_Password;
        private Label label2;
        private TextBox Txt_Amount;
        private Label label3;
        private GroupBox groupBox2;
        private Button Btn_SendTransaction;
        private TextBox Txt_TransactionHex;
        private Label label6;
        private GroupBox groupBox3;
        private TextBox TxtLog;
    }
}