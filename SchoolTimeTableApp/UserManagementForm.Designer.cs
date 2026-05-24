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
            this.labelTitle          = new System.Windows.Forms.Label();
            this.dataGridUsers       = new System.Windows.Forms.DataGridView();
            this.groupPassword       = new System.Windows.Forms.GroupBox();
            this.labelCurrent        = new System.Windows.Forms.Label();
            this.textCurrentPassword = new System.Windows.Forms.TextBox();
            this.buttonShowCurrent   = new System.Windows.Forms.Button();
            this.labelNew            = new System.Windows.Forms.Label();
            this.textNewPassword     = new System.Windows.Forms.TextBox();
            this.buttonShowNew       = new System.Windows.Forms.Button();
            this.buttonChangePwd     = new System.Windows.Forms.Button();
            this.groupRole           = new System.Windows.Forms.GroupBox();
            this.labelRoleHint       = new System.Windows.Forms.Label();
            this.comboRole           = new System.Windows.Forms.ComboBox();
            this.buttonChangeRole    = new System.Windows.Forms.Button();
            this.buttonClose         = new System.Windows.Forms.Button();
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
            this.labelTitle.Size      = new System.Drawing.Size(700, 36);
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
            this.dataGridUsers.MultiSelect     = false;
            this.dataGridUsers.Size            = new System.Drawing.Size(676, 300);
            this.dataGridUsers.TabIndex        = 1;
            this.dataGridUsers.SelectionChanged += new System.EventHandler(this.dataGridUsers_SelectionChanged);
            // groupPassword
            this.groupPassword.Controls.Add(this.labelCurrent);
            this.groupPassword.Controls.Add(this.textCurrentPassword);
            this.groupPassword.Controls.Add(this.buttonShowCurrent);
            this.groupPassword.Controls.Add(this.labelNew);
            this.groupPassword.Controls.Add(this.textNewPassword);
            this.groupPassword.Controls.Add(this.buttonShowNew);
            this.groupPassword.Controls.Add(this.buttonChangePwd);
            this.groupPassword.Location = new System.Drawing.Point(12, 362);
            this.groupPassword.Name     = "groupPassword";
            this.groupPassword.Size     = new System.Drawing.Size(420, 130);
            this.groupPassword.TabIndex = 2;
            this.groupPassword.Text     = "Выберите пользователя";
            // labelCurrent
            this.labelCurrent.AutoSize  = true;
            this.labelCurrent.ForeColor = System.Drawing.Color.Gray;
            this.labelCurrent.Location  = new System.Drawing.Point(6, 22);
            this.labelCurrent.Name      = "labelCurrent";
            this.labelCurrent.Text      = "Текущий пароль:";
            // textCurrentPassword
            this.textCurrentPassword.Location     = new System.Drawing.Point(6, 40);
            this.textCurrentPassword.Name         = "textCurrentPassword";
            this.textCurrentPassword.PasswordChar = '*';
            this.textCurrentPassword.ReadOnly     = true;
            this.textCurrentPassword.Size         = new System.Drawing.Size(340, 20);
            this.textCurrentPassword.TabIndex     = 0;
            this.textCurrentPassword.BackColor    = System.Drawing.Color.WhiteSmoke;
            // buttonShowCurrent
            this.buttonShowCurrent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShowCurrent.Location  = new System.Drawing.Point(352, 38);
            this.buttonShowCurrent.Name      = "buttonShowCurrent";
            this.buttonShowCurrent.Size      = new System.Drawing.Size(56, 24);
            this.buttonShowCurrent.TabIndex  = 1;
            this.buttonShowCurrent.Text      = "👁";
            this.buttonShowCurrent.UseVisualStyleBackColor = true;
            this.buttonShowCurrent.Click += new System.EventHandler(this.buttonShowCurrent_Click);
            // labelNew
            this.labelNew.AutoSize  = true;
            this.labelNew.ForeColor = System.Drawing.Color.Gray;
            this.labelNew.Location  = new System.Drawing.Point(6, 70);
            this.labelNew.Name      = "labelNew";
            this.labelNew.Text      = "Новый пароль:";
            // textNewPassword
            this.textNewPassword.Location     = new System.Drawing.Point(6, 88);
            this.textNewPassword.Name         = "textNewPassword";
            this.textNewPassword.PasswordChar = '*';
            this.textNewPassword.Size         = new System.Drawing.Size(240, 20);
            this.textNewPassword.TabIndex     = 2;
            // buttonShowNew
            this.buttonShowNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShowNew.Location  = new System.Drawing.Point(252, 86);
            this.buttonShowNew.Name      = "buttonShowNew";
            this.buttonShowNew.Size      = new System.Drawing.Size(46, 24);
            this.buttonShowNew.TabIndex  = 3;
            this.buttonShowNew.Text      = "👁";
            this.buttonShowNew.UseVisualStyleBackColor = true;
            this.buttonShowNew.Click += new System.EventHandler(this.buttonShowNew_Click);
            // buttonChangePwd
            this.buttonChangePwd.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonChangePwd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonChangePwd.ForeColor = System.Drawing.Color.White;
            this.buttonChangePwd.Location  = new System.Drawing.Point(306, 86);
            this.buttonChangePwd.Name      = "buttonChangePwd";
            this.buttonChangePwd.Size      = new System.Drawing.Size(102, 24);
            this.buttonChangePwd.TabIndex  = 4;
            this.buttonChangePwd.Text      = "Сменить пароль";
            this.buttonChangePwd.UseVisualStyleBackColor = false;
            this.buttonChangePwd.Click += new System.EventHandler(this.buttonChangePassword_Click);
            // groupRole
            this.groupRole.Controls.Add(this.labelRoleHint);
            this.groupRole.Controls.Add(this.comboRole);
            this.groupRole.Controls.Add(this.buttonChangeRole);
            this.groupRole.Location = new System.Drawing.Point(444, 362);
            this.groupRole.Name     = "groupRole";
            this.groupRole.Size     = new System.Drawing.Size(244, 130);
            this.groupRole.TabIndex = 3;
            this.groupRole.Text     = "Роль пользователя";
            // labelRoleHint
            this.labelRoleHint.AutoSize  = true;
            this.labelRoleHint.ForeColor = System.Drawing.Color.Gray;
            this.labelRoleHint.Location  = new System.Drawing.Point(6, 22);
            this.labelRoleHint.Name      = "labelRoleHint";
            this.labelRoleHint.Text      = "Назначить роль:";
            // comboRole
            this.comboRole.DropDownStyle    = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRole.FormattingEnabled = true;
            this.comboRole.Location         = new System.Drawing.Point(6, 40);
            this.comboRole.Name             = "comboRole";
            this.comboRole.Size             = new System.Drawing.Size(228, 21);
            this.comboRole.TabIndex         = 0;
            // buttonChangeRole
            this.buttonChangeRole.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonChangeRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonChangeRole.ForeColor = System.Drawing.Color.White;
            this.buttonChangeRole.Location  = new System.Drawing.Point(6, 88);
            this.buttonChangeRole.Name      = "buttonChangeRole";
            this.buttonChangeRole.Size      = new System.Drawing.Size(228, 24);
            this.buttonChangeRole.TabIndex  = 1;
            this.buttonChangeRole.Text      = "Назначить роль";
            this.buttonChangeRole.UseVisualStyleBackColor = false;
            this.buttonChangeRole.Click += new System.EventHandler(this.buttonChangeRole_Click);
            // buttonClose
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location  = new System.Drawing.Point(568, 506);
            this.buttonClose.Name      = "buttonClose";
            this.buttonClose.Size      = new System.Drawing.Size(120, 28);
            this.buttonClose.TabIndex  = 4;
            this.buttonClose.Text      = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // UserManagementForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(700, 546);
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
        private System.Windows.Forms.Label         labelCurrent;
        private System.Windows.Forms.TextBox       textCurrentPassword;
        private System.Windows.Forms.Button        buttonShowCurrent;
        private System.Windows.Forms.Label         labelNew;
        private System.Windows.Forms.TextBox       textNewPassword;
        private System.Windows.Forms.Button        buttonShowNew;
        private System.Windows.Forms.Button        buttonChangePwd;
        private System.Windows.Forms.GroupBox      groupRole;
        private System.Windows.Forms.Label         labelRoleHint;
        private System.Windows.Forms.ComboBox      comboRole;
        private System.Windows.Forms.Button        buttonChangeRole;
        private System.Windows.Forms.Button        buttonClose;
    }
}
