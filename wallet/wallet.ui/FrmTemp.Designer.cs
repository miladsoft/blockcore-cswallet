namespace wallet.ui
{
    partial class FrmTemp
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Wallet_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Confirmed_Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnConfirmed_Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Wallet_Name,
            this.Confirmed_Balance,
            this.UnConfirmed_Balance});
            this.dataGridView1.Location = new System.Drawing.Point(41, 112);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(718, 227);
            this.dataGridView1.TabIndex = 1;
            // 
            // Wallet_Name
            // 
            this.Wallet_Name.HeaderText = "Wallet Name";
            this.Wallet_Name.MinimumWidth = 6;
            this.Wallet_Name.Name = "Wallet_Name";
            this.Wallet_Name.ReadOnly = true;
            this.Wallet_Name.Width = 150;
            // 
            // Confirmed_Balance
            // 
            this.Confirmed_Balance.HeaderText = "Confirmed Balance";
            this.Confirmed_Balance.MinimumWidth = 6;
            this.Confirmed_Balance.Name = "Confirmed_Balance";
            this.Confirmed_Balance.ReadOnly = true;
            this.Confirmed_Balance.Width = 200;
            // 
            // UnConfirmed_Balance
            // 
            this.UnConfirmed_Balance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UnConfirmed_Balance.HeaderText = "UnConfirmed Balance";
            this.UnConfirmed_Balance.MinimumWidth = 6;
            this.UnConfirmed_Balance.Name = "UnConfirmed_Balance";
            this.UnConfirmed_Balance.ReadOnly = true;
            // 
            // FrmTemp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmTemp";
            this.Text = "FrmTemp";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Wallet_Name;
        private DataGridViewTextBoxColumn Confirmed_Balance;
        private DataGridViewTextBoxColumn UnConfirmed_Balance;
    }
}