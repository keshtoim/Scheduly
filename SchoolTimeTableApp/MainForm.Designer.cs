namespace testing
{
    partial class MainForm
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelAppTitle = new System.Windows.Forms.Label();
            this.labelUserInfo = new System.Windows.Forms.Label();
            this.buttonUsers     = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.buttonLogout   = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageSchedule = new System.Windows.Forms.TabPage();
            this.tabPageCompose = new System.Windows.Forms.TabPage();
            this.tabPageWorkload = new System.Windows.Forms.TabPage();
            this.tabPageReferences = new System.Windows.Forms.TabPage();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelTop.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.SteelBlue;
            this.panelTop.Controls.Add(this.buttonUsers);
            this.panelTop.Controls.Add(this.buttonSettings);
            this.panelTop.Controls.Add(this.buttonLogout);
            this.panelTop.Controls.Add(this.labelUserInfo);
            this.panelTop.Controls.Add(this.labelAppTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1200, 46);
            this.panelTop.TabIndex = 0;
            // 
            // labelAppTitle
            // 
            this.labelAppTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAppTitle.AutoSize = false;
            this.labelAppTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelAppTitle.ForeColor = System.Drawing.Color.White;
            this.labelAppTitle.Location = new System.Drawing.Point(10, 10);
            this.labelAppTitle.Name = "labelAppTitle";
            this.labelAppTitle.Size = new System.Drawing.Size(280, 24);
            this.labelAppTitle.TabIndex = 0;
            this.labelAppTitle.Text = "Школьное расписание";
            // 
            // labelUserInfo
            // 
            this.labelUserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right));
            this.labelUserInfo.AutoSize = false;
            this.labelUserInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelUserInfo.ForeColor = System.Drawing.Color.White;
            this.labelUserInfo.Location = new System.Drawing.Point(300, 14);
            this.labelUserInfo.Name = "labelUserInfo";
            this.labelUserInfo.Size = new System.Drawing.Size(780, 18);
            this.labelUserInfo.TabIndex = 1;
            this.labelUserInfo.Text = "—";
            // buttonUsers
            this.buttonUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right));
            this.buttonUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUsers.ForeColor = System.Drawing.Color.White;
            this.buttonUsers.Location = new System.Drawing.Point(862, 9);
            this.buttonUsers.Name = "buttonUsers";
            this.buttonUsers.Size = new System.Drawing.Size(106, 26);
            this.buttonUsers.TabIndex = 3;
            this.buttonUsers.Text = "👤 Пользователи";
            this.buttonUsers.UseVisualStyleBackColor = false;
            this.buttonUsers.Visible = false;
            this.buttonUsers.Click += new System.EventHandler(this.buttonUsers_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right));
            this.buttonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSettings.ForeColor = System.Drawing.Color.White;
            this.buttonSettings.Location = new System.Drawing.Point(976, 9);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(100, 26);
            this.buttonSettings.TabIndex = 3;
            this.buttonSettings.Text = "⚙ Настройки";
            this.buttonSettings.UseVisualStyleBackColor = false;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // buttonLogout
            // 
            this.buttonLogout.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right));
            this.buttonLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogout.ForeColor = System.Drawing.Color.White;
            this.buttonLogout.Location = new System.Drawing.Point(1088, 9);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(100, 26);
            this.buttonLogout.TabIndex = 2;
            this.buttonLogout.Text = "Выйти";
            this.buttonLogout.UseVisualStyleBackColor = false;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageSchedule);
            this.tabControl.Controls.Add(this.tabPageCompose);
            this.tabControl.Controls.Add(this.tabPageWorkload);
            this.tabControl.Controls.Add(this.tabPageReferences);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 46);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1200, 710);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageSchedule
            // 
            this.tabPageSchedule.Location = new System.Drawing.Point(4, 22);
            this.tabPageSchedule.Name = "tabPageSchedule";
            this.tabPageSchedule.Size = new System.Drawing.Size(1192, 684);
            this.tabPageSchedule.TabIndex = 0;
            this.tabPageSchedule.Text = "Расписание";
            this.tabPageSchedule.UseVisualStyleBackColor = true;
            // 
            // tabPageCompose
            // 
            this.tabPageCompose.Location = new System.Drawing.Point(4, 22);
            this.tabPageCompose.Name = "tabPageCompose";
            this.tabPageCompose.Size = new System.Drawing.Size(1192, 684);
            this.tabPageCompose.TabIndex = 1;
            this.tabPageCompose.Text = "Составление";
            this.tabPageCompose.UseVisualStyleBackColor = true;
            // 
            // tabPageWorkload
            // 
            this.tabPageWorkload.Location = new System.Drawing.Point(4, 22);
            this.tabPageWorkload.Name = "tabPageWorkload";
            this.tabPageWorkload.Size = new System.Drawing.Size(1192, 684);
            this.tabPageWorkload.TabIndex = 2;
            this.tabPageWorkload.Text = "Нагрузка";
            this.tabPageWorkload.UseVisualStyleBackColor = true;
            // 
            // tabPageReferences
            // 
            this.tabPageReferences.Location = new System.Drawing.Point(4, 22);
            this.tabPageReferences.Name = "tabPageReferences";
            this.tabPageReferences.Size = new System.Drawing.Size(1192, 684);
            this.tabPageReferences.TabIndex = 3;
            this.tabPageReferences.Text = "Справочники";
            this.tabPageReferences.UseVisualStyleBackColor = true;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 756);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1200, 22);
            this.statusStrip.TabIndex = 2;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Готово";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 778);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.statusStrip);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Школьное расписание";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelTop.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelAppTitle;
        private System.Windows.Forms.Label labelUserInfo;
        private System.Windows.Forms.Button buttonUsers;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageSchedule;
        private System.Windows.Forms.TabPage tabPageCompose;
        private System.Windows.Forms.TabPage tabPageWorkload;
        private System.Windows.Forms.TabPage tabPageReferences;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}
