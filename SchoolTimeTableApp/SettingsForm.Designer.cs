namespace testing
{
    partial class SettingsForm
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
            this.labelTitle     = new System.Windows.Forms.Label();
            this.labelHint      = new System.Windows.Forms.Label();
            this.dataGridLimits = new System.Windows.Forms.DataGridView();
            this.colClass       = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLimit       = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonDefaults = new System.Windows.Forms.Button();
            this.buttonSave     = new System.Windows.Forms.Button();
            this.buttonClose    = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridLimits)).BeginInit();
            this.SuspendLayout();
            // labelTitle
            this.labelTitle.AutoSize  = false;
            this.labelTitle.Font      = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelTitle.Location  = new System.Drawing.Point(12, 12);
            this.labelTitle.Name      = "labelTitle";
            this.labelTitle.Size      = new System.Drawing.Size(460, 28);
            this.labelTitle.TabIndex  = 0;
            this.labelTitle.Text      = "Настройки расписания";
            // labelHint
            this.labelHint.AutoSize  = false;
            this.labelHint.ForeColor = System.Drawing.Color.Gray;
            this.labelHint.Location  = new System.Drawing.Point(12, 44);
            this.labelHint.Name      = "labelHint";
            this.labelHint.Size      = new System.Drawing.Size(460, 34);
            this.labelHint.TabIndex  = 1;
            this.labelHint.Text      = "Максимальное количество уроков в день для каждого класса.\r\nПри превышении при добавлении урока будет показано предупреждение.";
            // dataGridLimits
            this.dataGridLimits.AllowUserToAddRows    = false;
            this.dataGridLimits.AllowUserToDeleteRows = false;
            this.dataGridLimits.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right));
            this.dataGridLimits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridLimits.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colClass, this.colLimit });
            this.dataGridLimits.Location      = new System.Drawing.Point(12, 86);
            this.dataGridLimits.Name          = "dataGridLimits";
            this.dataGridLimits.RowHeadersWidth = 40;
            this.dataGridLimits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridLimits.Size          = new System.Drawing.Size(460, 380);
            this.dataGridLimits.TabIndex      = 2;
            // colClass
            this.colClass.HeaderText = "Класс";
            this.colClass.Name       = "colClass";
            this.colClass.ReadOnly   = true;
            this.colClass.Width      = 120;
            // colLimit
            this.colLimit.HeaderText = "Макс. уроков в день";
            this.colLimit.Name       = "colLimit";
            this.colLimit.Width      = 180;
            // buttonDefaults
            this.buttonDefaults.Anchor    = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.buttonDefaults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDefaults.Location  = new System.Drawing.Point(12, 480);
            this.buttonDefaults.Name      = "buttonDefaults";
            this.buttonDefaults.Size      = new System.Drawing.Size(170, 28);
            this.buttonDefaults.TabIndex  = 3;
            this.buttonDefaults.Text      = "Значения по умолчанию";
            this.buttonDefaults.UseVisualStyleBackColor = true;
            this.buttonDefaults.Click += new System.EventHandler(this.buttonDefaults_Click);
            // buttonSave
            this.buttonSave.Anchor    = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.buttonSave.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location  = new System.Drawing.Point(212, 480);
            this.buttonSave.Name      = "buttonSave";
            this.buttonSave.Size      = new System.Drawing.Size(130, 28);
            this.buttonSave.TabIndex  = 4;
            this.buttonSave.Text      = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // buttonClose
            this.buttonClose.Anchor    = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location  = new System.Drawing.Point(352, 480);
            this.buttonClose.Name      = "buttonClose";
            this.buttonClose.Size      = new System.Drawing.Size(120, 28);
            this.buttonClose.TabIndex  = 5;
            this.buttonClose.Text      = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // SettingsForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(484, 520);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelHint);
            this.Controls.Add(this.dataGridLimits);
            this.Controls.Add(this.buttonDefaults);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.Name            = "SettingsForm";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Настройки";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridLimits)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Label            labelTitle;
        private System.Windows.Forms.Label            labelHint;
        private System.Windows.Forms.DataGridView     dataGridLimits;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLimit;
        private System.Windows.Forms.Button           buttonDefaults;
        private System.Windows.Forms.Button           buttonSave;
        private System.Windows.Forms.Button           buttonClose;
    }
}
