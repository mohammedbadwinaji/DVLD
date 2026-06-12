namespace DVLD
{
    partial class frmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.miUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.miPeople = new System.Windows.Forms.ToolStripMenuItem();
            this.miUserSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.miLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miApplication,
            this.miUsers,
            this.miPeople,
            this.miUserSettings});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 83);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miApplication
            // 
            this.miApplication.BackColor = System.Drawing.Color.White;
            this.miApplication.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.miApplication.Image = global::DVLD.Properties.Resources.Applications_64;
            this.miApplication.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miApplication.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.miApplication.Name = "miApplication";
            this.miApplication.Padding = new System.Windows.Forms.Padding(4);
            this.miApplication.Size = new System.Drawing.Size(165, 76);
            this.miApplication.Text = "Application";
            this.miApplication.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // miUsers
            // 
            this.miUsers.BackColor = System.Drawing.Color.White;
            this.miUsers.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.miUsers.Image = global::DVLD.Properties.Resources.Users_2_64;
            this.miUsers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miUsers.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.miUsers.Name = "miUsers";
            this.miUsers.Padding = new System.Windows.Forms.Padding(4);
            this.miUsers.Size = new System.Drawing.Size(124, 76);
            this.miUsers.Text = "Users";
            this.miUsers.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.miUsers.Click += new System.EventHandler(this.miUsers_Click);
            // 
            // miPeople
            // 
            this.miPeople.BackColor = System.Drawing.Color.White;
            this.miPeople.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.miPeople.Image = global::DVLD.Properties.Resources.People_64;
            this.miPeople.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miPeople.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.miPeople.Name = "miPeople";
            this.miPeople.Padding = new System.Windows.Forms.Padding(4);
            this.miPeople.Size = new System.Drawing.Size(132, 76);
            this.miPeople.Text = "People";
            this.miPeople.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.miPeople.Click += new System.EventHandler(this.miPeople_Click);
            // 
            // miUserSettings
            // 
            this.miUserSettings.BackColor = System.Drawing.Color.White;
            this.miUserSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLogout});
            this.miUserSettings.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.miUserSettings.Image = global::DVLD.Properties.Resources.account_settings_64;
            this.miUserSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miUserSettings.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.miUserSettings.Name = "miUserSettings";
            this.miUserSettings.Padding = new System.Windows.Forms.Padding(4);
            this.miUserSettings.Size = new System.Drawing.Size(204, 76);
            this.miUserSettings.Text = "Account Settings";
            this.miUserSettings.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // miLogout
            // 
            this.miLogout.Image = global::DVLD.Properties.Resources.Sign_Out_32;
            this.miLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.miLogout.Name = "miLogout";
            this.miLogout.Size = new System.Drawing.Size(128, 24);
            this.miLogout.Text = "Logout";
            this.miLogout.Click += new System.EventHandler(this.miLogout_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 506);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "DVLD";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miPeople;
        private System.Windows.Forms.ToolStripMenuItem miApplication;
        private System.Windows.Forms.ToolStripMenuItem miUserSettings;
        private System.Windows.Forms.ToolStripMenuItem miLogout;
        private System.Windows.Forms.ToolStripMenuItem miUsers;
    }
}

