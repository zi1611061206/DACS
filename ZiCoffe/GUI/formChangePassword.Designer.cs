namespace ZiCoffe.GUI
{
    partial class formChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formChangePassword));
            this.pnlOldPass = new System.Windows.Forms.Panel();
            this.txbOldPass = new System.Windows.Forms.TextBox();
            this.lbOldPass = new System.Windows.Forms.Label();
            this.pnlNewPass = new System.Windows.Forms.Panel();
            this.txbNewPass = new System.Windows.Forms.TextBox();
            this.lbNewPass = new System.Windows.Forms.Label();
            this.pnlReEnter = new System.Windows.Forms.Panel();
            this.txbReEnter = new System.Windows.Forms.TextBox();
            this.lbReEnter = new System.Windows.Forms.Label();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlOldPass.SuspendLayout();
            this.pnlNewPass.SuspendLayout();
            this.pnlReEnter.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOldPass
            // 
            this.pnlOldPass.Controls.Add(this.txbOldPass);
            this.pnlOldPass.Controls.Add(this.lbOldPass);
            this.pnlOldPass.Location = new System.Drawing.Point(12, 12);
            this.pnlOldPass.Name = "pnlOldPass";
            this.pnlOldPass.Size = new System.Drawing.Size(445, 54);
            this.pnlOldPass.TabIndex = 0;
            // 
            // txbOldPass
            // 
            this.txbOldPass.Location = new System.Drawing.Point(121, 13);
            this.txbOldPass.Name = "txbOldPass";
            this.txbOldPass.Size = new System.Drawing.Size(321, 26);
            this.txbOldPass.TabIndex = 1;
            this.txbOldPass.UseSystemPasswordChar = true;
            // 
            // lbOldPass
            // 
            this.lbOldPass.AutoSize = true;
            this.lbOldPass.Location = new System.Drawing.Point(3, 16);
            this.lbOldPass.Name = "lbOldPass";
            this.lbOldPass.Size = new System.Drawing.Size(99, 19);
            this.lbOldPass.TabIndex = 0;
            this.lbOldPass.Text = "Mật khẩu cũ:";
            // 
            // pnlNewPass
            // 
            this.pnlNewPass.Controls.Add(this.txbNewPass);
            this.pnlNewPass.Controls.Add(this.lbNewPass);
            this.pnlNewPass.Location = new System.Drawing.Point(12, 72);
            this.pnlNewPass.Name = "pnlNewPass";
            this.pnlNewPass.Size = new System.Drawing.Size(445, 54);
            this.pnlNewPass.TabIndex = 6;
            // 
            // txbNewPass
            // 
            this.txbNewPass.Location = new System.Drawing.Point(121, 13);
            this.txbNewPass.Name = "txbNewPass";
            this.txbNewPass.Size = new System.Drawing.Size(321, 26);
            this.txbNewPass.TabIndex = 2;
            this.txbNewPass.UseSystemPasswordChar = true;
            // 
            // lbNewPass
            // 
            this.lbNewPass.AutoSize = true;
            this.lbNewPass.Location = new System.Drawing.Point(3, 16);
            this.lbNewPass.Name = "lbNewPass";
            this.lbNewPass.Size = new System.Drawing.Size(109, 19);
            this.lbNewPass.TabIndex = 0;
            this.lbNewPass.Text = "Mật khẩu mới:";
            // 
            // pnlReEnter
            // 
            this.pnlReEnter.Controls.Add(this.txbReEnter);
            this.pnlReEnter.Controls.Add(this.lbReEnter);
            this.pnlReEnter.Location = new System.Drawing.Point(12, 132);
            this.pnlReEnter.Name = "pnlReEnter";
            this.pnlReEnter.Size = new System.Drawing.Size(445, 54);
            this.pnlReEnter.TabIndex = 10;
            // 
            // txbReEnter
            // 
            this.txbReEnter.Location = new System.Drawing.Point(121, 13);
            this.txbReEnter.Name = "txbReEnter";
            this.txbReEnter.Size = new System.Drawing.Size(321, 26);
            this.txbReEnter.TabIndex = 3;
            this.txbReEnter.UseSystemPasswordChar = true;
            // 
            // lbReEnter
            // 
            this.lbReEnter.AutoSize = true;
            this.lbReEnter.Location = new System.Drawing.Point(3, 16);
            this.lbReEnter.Name = "lbReEnter";
            this.lbReEnter.Size = new System.Drawing.Size(70, 19);
            this.lbReEnter.TabIndex = 0;
            this.lbReEnter.Text = "Nhập lại:";
            // 
            // pnlButton
            // 
            this.pnlButton.Controls.Add(this.btnCancel);
            this.pnlButton.Controls.Add(this.btnSave);
            this.pnlButton.Location = new System.Drawing.Point(12, 192);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(445, 38);
            this.pnlButton.TabIndex = 11;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(230, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(113, 31);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(102, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(113, 31);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // formChangePassword
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(469, 232);
            this.Controls.Add(this.pnlButton);
            this.Controls.Add(this.pnlReEnter);
            this.Controls.Add(this.pnlNewPass);
            this.Controls.Add(this.pnlOldPass);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "formChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đổi mật khẩu";
            this.pnlOldPass.ResumeLayout(false);
            this.pnlOldPass.PerformLayout();
            this.pnlNewPass.ResumeLayout(false);
            this.pnlNewPass.PerformLayout();
            this.pnlReEnter.ResumeLayout(false);
            this.pnlReEnter.PerformLayout();
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlOldPass;
        private System.Windows.Forms.TextBox txbOldPass;
        private System.Windows.Forms.Label lbOldPass;
        private System.Windows.Forms.Panel pnlNewPass;
        private System.Windows.Forms.TextBox txbNewPass;
        private System.Windows.Forms.Label lbNewPass;
        private System.Windows.Forms.Panel pnlReEnter;
        private System.Windows.Forms.TextBox txbReEnter;
        private System.Windows.Forms.Label lbReEnter;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}