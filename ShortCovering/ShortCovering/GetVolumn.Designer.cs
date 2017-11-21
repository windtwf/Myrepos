namespace ShortCovering
{
    partial class FrmGetVolumn
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
            this.btnQueryBill = new System.Windows.Forms.Button();
            this.txtStockID = new System.Windows.Forms.TextBox();
            this.txtBill = new System.Windows.Forms.TextBox();
            this.comboHand = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnQueryBill
            // 
            this.btnQueryBill.Location = new System.Drawing.Point(262, 35);
            this.btnQueryBill.Name = "btnQueryBill";
            this.btnQueryBill.Size = new System.Drawing.Size(110, 23);
            this.btnQueryBill.TabIndex = 0;
            this.btnQueryBill.Text = "Get Bill List";
            this.btnQueryBill.UseVisualStyleBackColor = true;
            this.btnQueryBill.Click += new System.EventHandler(this.btnQueryBill_Click);
            // 
            // txtStockID
            // 
            this.txtStockID.Location = new System.Drawing.Point(77, 38);
            this.txtStockID.Name = "txtStockID";
            this.txtStockID.Size = new System.Drawing.Size(100, 20);
            this.txtStockID.TabIndex = 1;
            // 
            // txtBill
            // 
            this.txtBill.Location = new System.Drawing.Point(29, 104);
            this.txtBill.Multiline = true;
            this.txtBill.Name = "txtBill";
            this.txtBill.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBill.Size = new System.Drawing.Size(513, 300);
            this.txtBill.TabIndex = 2;
            // 
            // comboHand
            // 
            this.comboHand.FormattingEnabled = true;
            this.comboHand.Items.AddRange(new object[] {
            "10000",
            "5000",
            "2000",
            "1000"});
            this.comboHand.Location = new System.Drawing.Point(459, 35);
            this.comboHand.Name = "comboHand";
            this.comboHand.Size = new System.Drawing.Size(121, 21);
            this.comboHand.TabIndex = 3;
            this.comboHand.SelectedIndexChanged += new System.EventHandler(this.comboHand_SelectedIndexChanged);
            // 
            // FrmGetVolumn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 576);
            this.Controls.Add(this.comboHand);
            this.Controls.Add(this.txtBill);
            this.Controls.Add(this.txtStockID);
            this.Controls.Add(this.btnQueryBill);
            this.Name = "FrmGetVolumn";
            this.Text = "GetVolumn";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnQueryBill;
        private System.Windows.Forms.TextBox txtStockID;
        private System.Windows.Forms.TextBox txtBill;
        private System.Windows.Forms.ComboBox comboHand;
    }
}