namespace DVLD.Forms.Users
{
    partial class frmUserManagement
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
            this.cmbFilterOptions = new System.Windows.Forms.ComboBox();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiShowDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiSendEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiPhoneCall = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddNewUser = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlPeopleView = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbFilterValue = new System.Windows.Forms.ComboBox();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.cms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlPeopleView.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbFilterOptions
            // 
            this.cmbFilterOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterOptions.FormattingEnabled = true;
            this.cmbFilterOptions.Location = new System.Drawing.Point(119, 12);
            this.cmbFilterOptions.Name = "cmbFilterOptions";
            this.cmbFilterOptions.Size = new System.Drawing.Size(158, 21);
            this.cmbFilterOptions.TabIndex = 2;
            this.cmbFilterOptions.SelectedIndexChanged += new System.EventHandler(this.cmbFilterOptions_SelectedIndexChanged);
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.ContextMenuStrip = this.cms;
            this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvUsers.Location = new System.Drawing.Point(0, 50);
            this.dgvUsers.MultiSelect = false;
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.Size = new System.Drawing.Size(922, 217);
            this.dgvUsers.TabIndex = 0;
            this.dgvUsers.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvUsers_CellMouseDown);
            // 
            // cms
            // 
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiShowDetails,
            this.cmiAdd,
            this.cmiEdit,
            this.cmiDelete,
            this.cmiChangePassword,
            this.cmiSendEmail,
            this.cmiPhoneCall});
            this.cms.Name = "contextMenuStrip1";
            this.cms.Size = new System.Drawing.Size(181, 180);
            // 
            // cmiAdd
            // 
            this.cmiAdd.Image = global::DVLD.Properties.Resources.AddPerson_32;
            this.cmiAdd.Name = "cmiAdd";
            this.cmiAdd.Size = new System.Drawing.Size(180, 22);
            this.cmiAdd.Text = "Add";
            this.cmiAdd.Click += new System.EventHandler(this.cmiAdd_Click);
            // 
            // cmiEdit
            // 
            this.cmiEdit.Image = global::DVLD.Properties.Resources.edit_32;
            this.cmiEdit.Name = "cmiEdit";
            this.cmiEdit.Size = new System.Drawing.Size(180, 22);
            this.cmiEdit.Text = "Edit";
            this.cmiEdit.Click += new System.EventHandler(this.cmiEdit_Click);
            // 
            // cmiDelete
            // 
            this.cmiDelete.Image = global::DVLD.Properties.Resources.Delete_32;
            this.cmiDelete.Name = "cmiDelete";
            this.cmiDelete.Size = new System.Drawing.Size(180, 22);
            this.cmiDelete.Text = "Delete";
            // 
            // cmiShowDetails
            // 
            this.cmiShowDetails.Image = global::DVLD.Properties.Resources.PersonDetails_32;
            this.cmiShowDetails.Name = "cmiShowDetails";
            this.cmiShowDetails.Size = new System.Drawing.Size(180, 22);
            this.cmiShowDetails.Text = "Show Details";
            this.cmiShowDetails.Click += new System.EventHandler(this.cmiShowDetails_Click);
            // 
            // cmiChangePassword
            // 
            this.cmiChangePassword.Image = global::DVLD.Properties.Resources.PersonDetails_32;
            this.cmiChangePassword.Name = "cmiChangePassword";
            this.cmiChangePassword.Size = new System.Drawing.Size(180, 22);
            this.cmiChangePassword.Text = "Change Password";
            this.cmiChangePassword.Click += new System.EventHandler(this.cmiChangePassword_Click);
            // 
            // cmiSendEmail
            // 
            this.cmiSendEmail.Image = global::DVLD.Properties.Resources.send_email_32;
            this.cmiSendEmail.Name = "cmiSendEmail";
            this.cmiSendEmail.Size = new System.Drawing.Size(180, 22);
            this.cmiSendEmail.Text = "Send Email";
            // 
            // cmiPhoneCall
            // 
            this.cmiPhoneCall.Image = global::DVLD.Properties.Resources.call_321;
            this.cmiPhoneCall.Name = "cmiPhoneCall";
            this.cmiPhoneCall.Size = new System.Drawing.Size(180, 22);
            this.cmiPhoneCall.Text = "Phone Call";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Filter By : ";
            // 
            // btnAddNewUser
            // 
            this.btnAddNewUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNewUser.Image = global::DVLD.Properties.Resources.Add_Person_40;
            this.btnAddNewUser.Location = new System.Drawing.Point(830, 3);
            this.btnAddNewUser.Name = "btnAddNewUser";
            this.btnAddNewUser.Size = new System.Drawing.Size(67, 42);
            this.btnAddNewUser.TabIndex = 0;
            this.btnAddNewUser.UseVisualStyleBackColor = true;
            this.btnAddNewUser.Click += new System.EventHandler(this.btnAddNewUser_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Users_2_400;
            this.pictureBox1.Location = new System.Drawing.Point(433, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(119, 78);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pnlPeopleView
            // 
            this.pnlPeopleView.Controls.Add(this.panel1);
            this.pnlPeopleView.Controls.Add(this.dgvUsers);
            this.pnlPeopleView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPeopleView.Location = new System.Drawing.Point(0, 132);
            this.pnlPeopleView.Name = "pnlPeopleView";
            this.pnlPeopleView.Size = new System.Drawing.Size(922, 267);
            this.pnlPeopleView.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbFilterValue);
            this.panel1.Controls.Add(this.txtFilterValue);
            this.panel1.Controls.Add(this.cmbFilterOptions);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnAddNewUser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(922, 49);
            this.panel1.TabIndex = 1;
            // 
            // cmbFilterValue
            // 
            this.cmbFilterValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterValue.FormattingEnabled = true;
            this.cmbFilterValue.Location = new System.Drawing.Point(300, 12);
            this.cmbFilterValue.Name = "cmbFilterValue";
            this.cmbFilterValue.Size = new System.Drawing.Size(216, 21);
            this.cmbFilterValue.TabIndex = 4;
            this.cmbFilterValue.SelectedIndexChanged += new System.EventHandler(this.cmbFilterValue_SelectedIndexChanged);
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.Location = new System.Drawing.Point(300, 12);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(216, 20);
            this.txtFilterValue.TabIndex = 3;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(115, 15);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(0, 24);
            this.lblTotal.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Total : ";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.lblTotal);
            this.pnlFooter.Controls.Add(this.label3);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 399);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(922, 46);
            this.pnlFooter.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(406, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Manage Users";
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Controls.Add(this.pictureBox1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(922, 132);
            this.pnlHeader.TabIndex = 3;
            // 
            // frmUserManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 445);
            this.Controls.Add(this.pnlPeopleView);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.Name = "frmUserManagement";
            this.Text = "Manage Users";
            this.Load += new System.EventHandler(this.frmUserManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.cms.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlPeopleView.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbFilterOptions;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem cmiAdd;
        private System.Windows.Forms.ToolStripMenuItem cmiEdit;
        private System.Windows.Forms.ToolStripMenuItem cmiDelete;
        private System.Windows.Forms.ToolStripMenuItem cmiShowDetails;
        private System.Windows.Forms.ToolStripMenuItem cmiSendEmail;
        private System.Windows.Forms.ToolStripMenuItem cmiPhoneCall;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddNewUser;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlPeopleView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.ComboBox cmbFilterValue;
        private System.Windows.Forms.ToolStripMenuItem cmiChangePassword;
    }
}