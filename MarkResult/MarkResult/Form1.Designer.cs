namespace MarkResult
{
    partial class Form1
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
            this.txtSuitID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProjectID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btn_trx = new System.Windows.Forms.Button();
            this.txt_trx = new System.Windows.Forms.TextBox();
            this.ofd_Trx = new System.Windows.Forms.OpenFileDialog();
            this.btnPlayList = new System.Windows.Forms.Button();
            this.txtPlaylist = new System.Windows.Forms.TextBox();
            this.ofd_playlist = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // txtSuitID
            // 
            this.txtSuitID.Location = new System.Drawing.Point(96, 66);
            this.txtSuitID.Name = "txtSuitID";
            this.txtSuitID.Size = new System.Drawing.Size(100, 20);
            this.txtSuitID.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Suid ID";
            // 
            // txtProjectID
            // 
            this.txtProjectID.Location = new System.Drawing.Point(96, 29);
            this.txtProjectID.Name = "txtProjectID";
            this.txtProjectID.Size = new System.Drawing.Size(100, 20);
            this.txtProjectID.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Project ID";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(378, 34);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(86, 36);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btn_trx
            // 
            this.btn_trx.Location = new System.Drawing.Point(22, 125);
            this.btn_trx.Name = "btn_trx";
            this.btn_trx.Size = new System.Drawing.Size(112, 25);
            this.btn_trx.TabIndex = 7;
            this.btn_trx.Text = "Select .trx File";
            this.btn_trx.UseVisualStyleBackColor = true;
            this.btn_trx.Click += new System.EventHandler(this.btn_trx_Click);
            // 
            // txt_trx
            // 
            this.txt_trx.Location = new System.Drawing.Point(187, 125);
            this.txt_trx.Name = "txt_trx";
            this.txt_trx.Size = new System.Drawing.Size(360, 20);
            this.txt_trx.TabIndex = 8;
            // 
            // ofd_Trx
            // 
            this.ofd_Trx.FileName = "openFileDialogTrx";
            // 
            // btnPlayList
            // 
            this.btnPlayList.Location = new System.Drawing.Point(22, 168);
            this.btnPlayList.Name = "btnPlayList";
            this.btnPlayList.Size = new System.Drawing.Size(112, 24);
            this.btnPlayList.TabIndex = 9;
            this.btnPlayList.Text = "Generate PlayList File";
            this.btnPlayList.UseVisualStyleBackColor = true;
            this.btnPlayList.Click += new System.EventHandler(this.txtPlayList_Click);
            // 
            // txtPlaylist
            // 
            this.txtPlaylist.Location = new System.Drawing.Point(187, 168);
            this.txtPlaylist.Name = "txtPlaylist";
            this.txtPlaylist.Size = new System.Drawing.Size(360, 20);
            this.txtPlaylist.TabIndex = 10;
            // 
            // ofd_playlist
            // 
            this.ofd_playlist.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 208);
            this.Controls.Add(this.txtPlaylist);
            this.Controls.Add(this.btnPlayList);
            this.Controls.Add(this.txt_trx);
            this.Controls.Add(this.btn_trx);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtProjectID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSuitID);
            this.Name = "Form1";
            this.Text = "MarkResult";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSuitID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProjectID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btn_trx;
        private System.Windows.Forms.TextBox txt_trx;
        private System.Windows.Forms.OpenFileDialog ofd_Trx;
        private System.Windows.Forms.Button btnPlayList;
        private System.Windows.Forms.TextBox txtPlaylist;
        private System.Windows.Forms.OpenFileDialog ofd_playlist;
    }
}

