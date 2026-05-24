namespace testing
{
    partial class UserManagementForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.labelTitle       = new System.Windows.Forms.Label();
            this.dataGridUsers    = new System.Windows.Forms.DataGridView();
            this.groupPassword    = new System.Windows.Forms.GroupBox();
            this.labelPwdHint     = new System.Windows.Forms.Label();
            this.textNewPassword  = new System.Windows.Forms.TextBox();
            this.buttonChangePwd  = new System.Windows.Forms.Button();
            this.groupRole        = new System.Windows.Forms.GroupBox();
            this.labelRoleHint    = new System.Windows.Forms.Label();
            this.comboRole        = new System.Windows.Forms.ComboBox();
            this.buttonChangeRole = new System.Windows.Forms.Button();
            this.buttonClose      = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUsers)).BeginInit();
            this.groupPassword.SuspendLayout();
            this.groupRole.SuspendLayout();
            this.SuspendLayout();
            // labelTitle
            this.labelTitle.AutoSize  = false;
            this.labelTitle.BackColor = System.Drawing.Color.SteelBlue;
            this.labelTitle.Dock      = System.Windows.Forms.DockStyle.Top;
            this.labelTitle.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location  = new System.Drawing.Point(0, 0);
            this.labelTitle.Name      = "labelTitle";
            this.labelTitle.Size      = new System.Drawing.Size(680, 36);
            this.labelTitle.TabIndex  = 0;
            this.labelTitle.Text      = "  Управление пользователями";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // dataGridUsers
            this.dataGridUsers.AllowUserToAddRows    = false;
            this.dataGridUsers.AllowUserToDeleteRows = false;
            this.dataGridUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right));
            this.dataGridUsers.Location        = new System.Drawing.Point(12, 48);
            this.dataGridUsers.Name            = "dataGridUsers";
            this.dataGridUsers.ReadOnly        = true;
            this.dataGridUsers.RowHeadersWidth = 40;
            this.dataGridUsers.SelectionMode   = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridUsers.Size            = new System.Drawing.Size(656, 300);
            this.dataGridUsers.TabIndex        = 1;
            // groupPassword
            this.groupPassword.Controls.Add(this.labelPwdHint);
            this.groupPassword.Controls.Add(this.textNewPassword);
            this.groupPassword.Controls.Add(this.buttonChangePwd);
            this.groupPassword.Location = new System.Drawing.Point(12, 362);
            this.groupPassword.Name     = "groupPassword";
            this.groupPassword.Size     = new System.Drawing.Size(320, 90);
            this.groupPassword.TabIndex = 2;
            this.groupPassword.Text     = "Сменить пароль выбранному пользователю";
            // labelPwdHint
            this.labelPwdHint.AutoSize = true;
            this.labelPwdHint.ForeColor = System.Drawing.Color.Gray;
            this.labelPwdHint.Location = new System.Drawing.Point(6, 22);
            this.labelPwdHint.Name     = "labelPwdHint";
            this.labelPwdHint.Text     = "Новый пароль:";
            // textNewPassword
            this.textNewPassword.Location     = new System.Drawing.Point(6, 42);
            this.textNewPassword.Name         = "textNewPassword";
            this.textNewPassword.PasswordChar = '*';
            this.textNewPassword.Size         = new System.Drawing.Size(200, 20);
            this.textNewPassword.TabIndex     = 0;
            // buttonChangePwd
            this.buttonChangePwd.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonChangePwd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonChangePwd.ForeColor = System.Drawing.Color.White;
            this.buttonChangePwd.Location  = new System.Drawing.Point(214, 40);
            this.buttonChangePwd.Name      = "buttonChangePwd";
            this.buttonChangePwd.Size      = new System.Drawing.Size(96, 24);
            this.buttonChangePwd.TabIndex  = 1;
            this.buttonChangePwd.Text      = "Изменить";
            this.buttonChangePwd.UseVisualStyleBackColor = false;
            this.buttonChangePwd.Click += new System.EventHandler(this.buttonChangePassword_Click);
            // groupRole
            this.groupRole.Controls.Add(this.labelRoleHint);
            this.groupRole.Controls.Add(this.comboRole);
            this.groupRole.Controls.Add(this.buttonChangeRole);
            this.groupRole.Location = new System.Drawing.Point(348, 362);
            this.groupRole.Name     = "groupRole";
            this.groupRole.Size     = new System.Drawing.Size(320, 90);
            this.groupRole.TabIndex = 3;
            this.groupRole.Text     = "Изменить роль выбранному пользователю";
            // labelRoleHint
            this.labelRoleHint.AutoSize  = true;
            this.labelRoleHint.ForeColor = System.Drawing.Color.Gray;
            this.labelRoleHint.Location  = new System.Drawing.Point(6, 22);
            this.labelRoleHint.Name      = "labelRoleHint";
            this.labelRoleHint.Text      = "Новая роль:";
            // comboRole
            this.comboRole.DropDownStyle    = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRole.FormattingEnabled = true;
            this.comboRole.Location         = new System.Drawing.Point(6, 42);
            this.comboRole.Name             = "comboRole";
            this.comboRole.Size             = new System.Drawing.Size(200, 21);
            this.comboRole.TabIndex         = 0;
            // buttonChangeRole
            this.buttonChangeRole.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonChangeRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonChangeRole.ForeColor = System.Drawing.Color.White;
            this.buttonChangeRole.Location  = new System.Drawing.Point(214, 40);
            this.buttonChangeRole.Name      = "buttonChangeRole";
            this.buttonChangeRole.Size      = new System.Drawing.Size(96, 24);
            this.buttonChangeRole.TabIndex  = 1;
            this.buttonChangeRole.Text      = "Назначить";
            this.buttonChangeRole.UseVisualStyleBackColor = false;
            this.buttonChangeRole.Click += new System.EventHandler(this.buttonChangeRole_Click);
            // buttonClose
            this.buttonClose.Anchor    = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location  = new System.Drawing.Point(556, 468);
            this.buttonClose.Name      = "buttonClose";
            this.buttonClose.Size      = new System.Drawing.Size(112, 28);
            this.buttonClose.TabIndex  = 4;
            this.buttonClose.Text      = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // UserManagementForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(680, 508);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.dataGridUsers);
            this.Controls.Add(this.groupPassword);
            this.Controls.Add(this.groupRole);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.Name            = "UserManagementForm";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Управление пользователями";
            this.Load += new System.EventHandler(this.UserManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUsers)).EndInit();
            this.groupPassword.ResumeLayout(false);
            this.groupPassword.PerformLayout();
            this.groupRole.ResumeLayout(false);
            this.groupRole.PerformLayout();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Label         labelTitle;
        private System.Windows.Forms.DataGridView  dataGridUsers;
        private System.Windows.Forms.GroupBox      groupPassword;
        private System.Windows.Forms.Label         labelPwdHint;
        private System.Windows.Forms.TextBox       textNewPassword;
        private System.Windows.Forms.Button        buttonChangePwd;
        private System.Windows.Forms.GroupBox      groupRole;
        private System.Windows.Forms.Label         labelRoleHint;
        private System.Windows.Forms.ComboBox      comboRole;
        private System.Windows.Forms.Button        buttonChangeRole;
        private System.Windows.Forms.Button        buttonClose;
    }
}
