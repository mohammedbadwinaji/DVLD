namespace DVLD.Forms.Users
{
    partial class frmUserDetails
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
            this.ctrlUserCard = new DVLD.Controls.usrUserCard();
            this.SuspendLayout();
            // 
            // ctrlUserCard
            // 
            this.ctrlUserCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlUserCard.Location = new System.Drawing.Point(0, 0);
            this.ctrlUserCard.Name = "ctrlUserCard";
            this.ctrlUserCard.Size = new System.Drawing.Size(698, 450);
            this.ctrlUserCard.TabIndex = 0;
            // 
            // frmUserCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 450);
            this.Controls.Add(this.ctrlUserCard);
            this.Name = "frmUserCard";
            this.Text = "frmUserCard";
            this.Load += new System.EventHandler(this.frmUserCard_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.usrUserCard ctrlUserCard;
    }
}